using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4.Binary
{
    class Reader {
        public static uint ReadUInt32(byte[] message, int offset) {
            uint data = 0;

            data |= message[offset++];
            data |= (uint)message[offset++] << 8;
            data |= (uint)message[offset++] << 16;
            data |= (uint)message[offset++] << 24;

            return data;
        }
        public static string ReadString(byte[] message, int offset, int length) {
            string data = "";

            for (int i = offset; i < offset + length; i++) {
                char letter = (char)message[i];
                if (letter == 0) break;
                data += letter;
            }
            return data;
        }
    }
}
