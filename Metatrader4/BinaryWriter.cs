using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4 {
    class BinaryWriter {
        public static byte[] Write8(string data) {
            byte[] buffer = new byte[data.Length];

            for (int i = 0; i < buffer.Length; i++) {
                buffer[i] = (byte)data[i];
            }

            return buffer;
;        }

        public static byte[] Write16(string data) {
            byte[] buffer = new byte[data.Length * 2];
            int offset = 0;

            for (int i = 0; i < data.Length; i++) {
                byte[] charBytes = BitConverter.GetBytes((short)data[i]);
                buffer[offset++] = charBytes[0];
                buffer[offset++] = charBytes[1];
            }

            return buffer;
        }

        public static byte[] Write32(string data) {
            byte[] buffer = new byte[data.Length * 4];
            int offset = 0;

            for (int i = 0; i < data.Length; i++) {
                byte[] charBytes = BitConverter.GetBytes((int)data[i]);
                buffer[offset++] = charBytes[0];
                buffer[offset++] = charBytes[1];
                buffer[offset++] = charBytes[2];
                buffer[offset++] = charBytes[3];

            }

            return buffer;
        }
    }
}
