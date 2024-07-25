using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4
{
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

        public MT4() {
        }

        public byte[] Token() {
            byte[] test = { 0 };
            return test;
        }

        public byte[] Encrypt(byte[] bytes, byte[] key, byte[] iv) {
            using (Aes aes = Aes.Create()) {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, IV);
                using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                }
                   return ms.ToArray();
                }
            }
        }


        public byte[] Decrypt(byte[] ciphertextBytes, byte[] key, byte[] iv) {
            using (Aes aes = Aes.Create()) {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, IV);

                using (MemoryStream ms = new MemoryStream(ciphertextBytes)) 
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) 
                using (MemoryStream decryptedMs = new MemoryStream()) {
                    cs.CopyTo(decryptedMs);
                    return decryptedMs.ToArray();
                }

            }
        }
    }
}
