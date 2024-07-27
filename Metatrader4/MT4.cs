using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Metatrader4.Binary;
using Metatrader4.Order;

namespace Metatrader4
{
    class MT4 {
        static string pE = "13ef13b2b76dd8:5795gdcfb2fdc1ge85bf768f54773d22fff996e3ge75g5:75";
        private byte[] FirstEncryptionKey = NormaliseKey(CreateKey(MT4.pE));
        private byte[] WindowsSpecification = NormaliseKey("67068786ddd67fb402e56d865f299372");
        private byte[] IV = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static string CreateKey(string key) {
            var result = new List<char>();

            for (int index = 0; index < key.Length; index++) {
                int charCode = (int)key[index];
                if (charCode == 28) result.Add('&');
                else if (charCode == 23) result.Add('!');
                else result.Add((char)(charCode - 1));
            }

            return new string(result.ToArray());
        }

        static byte[] NormaliseKey(string key) {
            var result = new List<byte>();
            int halfLength = key.Length / 2;
            for (int index = 0; index < halfLength; index++) {
                string hexSubstring = key.Substring(2 * index, 2);
                if (!string.IsNullOrEmpty(hexSubstring))
                    result.Add(Convert.ToByte(hexSubstring, 16));
            }
            return result.ToArray();
        }

        private Random random;

        private byte[] key;
        private string token;
        private string password;

        public MT4(AccountResponse account) {
            random = new Random();
            key = MT4.NormaliseKey(account.key);
            token = account.token;
            password = account.password;
        }

        public byte[] Token() {
            byte[] token = Writer.Write8(this.token);
            byte[] token_init = Init(0, token);
            byte[] encrypted_token = Encrypt(token_init, FirstEncryptionKey, IV);
            return encrypted_token;
        }

        public byte[] Password() {
            byte[] password = Writer.Write16(this.password, 64 + 16);

            int offset = 64;
            for (int i = 0; i < 16; i++, offset++) {
                password[offset] = WindowsSpecification[i];
            }

            byte[] passwordBuffer = Init(1, password);

            return Encrypt(passwordBuffer, key, IV);
        }

        public byte[] InitOrder(OrderResponse order) {
            byte[] OrderBuffer = new byte[95];
            int offset = 0;
            OrderBuffer[offset] = 66; // Market execution

            OrderBuffer[1] = order.direction; 

            byte[] asset = Writer.WriteString(order.asset);

            offset += 11;

            for (int i = 0; i < asset.Length; i++) {
                OrderBuffer[offset++] = asset[i];
            }

            offset += 4;

            OrderBuffer[offset++] = (byte)(order.volume & 0xFF);
            OrderBuffer[offset++] = (byte)((order.volume >> 8) & 0xFF);
            OrderBuffer[offset++] = (byte)((order.volume >> 16) & 0xFF);
            OrderBuffer[offset++] = (byte)((order.volume >> 24) & 0xFF);

            offset = 91;

            OrderBuffer[offset++] = 233;
            OrderBuffer[offset++] = 3;

            OrderBuffer[23] = 1;
          
            byte[] InitOrderBuffer = Init(12, OrderBuffer);
            return Encrypt(InitOrderBuffer, key, IV);
        }

        public byte[] Init(int opcode) {
            byte[] buffer = new byte[4];

            int offset = 0;
            buffer[offset++] = GetRandomByte();
            buffer[offset++] = GetRandomByte();
            buffer[offset++] = (byte)opcode;
            buffer[offset++] = 0;

            return buffer;
        }

        public byte[] Init(int opcode, byte[] token) {
            byte[] buffer = new byte[4 + token.Length];

            int offset = 0;
            buffer[offset++] = GetRandomByte();
            buffer[offset++] = GetRandomByte();
            buffer[offset++] = (byte)opcode;
            buffer[offset++] = 0;

            for (int i = 0; i < token.Length; i++) {
                buffer[offset++] = token[i]; 
            }

            return buffer;
        }

        public byte GetRandomByte() {
            return (byte)Math.Floor(0xFF * random.NextDouble());
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv) {
            using (var aes = Aes.Create()) {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.Zeros;

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV)) {
                    return PerformCryptography(data, encryptor);
                }
            }
        }


        public byte[] Decrypt(byte[] data) {
            using (var aes = Aes.Create()) {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.Zeros;

                aes.Key = this.key;
                aes.IV = IV;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV)) {
                    return PerformCryptography(data, decryptor);
                }
            }
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform) {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write)) {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }


    }
}
