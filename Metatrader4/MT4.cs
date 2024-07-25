using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4 {
    class MT4 {
        static string pE = "13ef13b2b76dd8:5795gdcfb2fdc1ge85bf768f54773d22fff996e3ge75g5:75";
        private byte[] FirstEncryptionKey = NormaliseKey(CreateKey(MT4.pE));
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

        public MT4(AccountResponse account) {
            random = new Random();
            key = MT4.NormaliseKey(account.key);
            token = account.token;
        }

        public byte[] Token() {
            byte[] token = BinaryWriter.Write8(this.token);
            byte[] token_init = Init(0, token);
            byte[] encrypted_token = Encrypt(token_init, FirstEncryptionKey, IV);
            return encrypted_token;
        }

        public byte[] Password() {

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

                aes.Key = key;
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
