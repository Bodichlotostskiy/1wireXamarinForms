using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Utils
{
    public class Address
    {/// <summary>
     /// Converts a 1-Wire Network address byte array (little endian)
     /// to a hex string representation (big endian).
     /// </summary>
     /// <param name="address">address, family code first</param>
     /// <returns>address represented in a string, family code last.</returns>
        public static string ToString(byte[] address)
        {
            // When displaying, the CRC is first, family code is last so
            // that the center 6 bytes are a real serial number (not byte reversed).

            char[] barr = new char[16];
            int index = 0;
            int ch;

            for (int i = 7; i >= 0; i--)
            {
                ch = (address[i] >> 4) & 0x0F;
                ch += ((ch > 9) ? 'A' - 10 : '0');
                barr[index++] = (char)ch;
                ch = address[i] & 0x0F;
                ch += ((ch > 9) ? 'A' - 10 : '0');
                barr[index++] = (char)ch;
            }
            return new string(barr);
        }

        public static byte[] ToByteArray(string address)
        {
            if (address.Length != 16)
                throw new OneWireException("Invalid address");
            byte[] ba = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                ba[7 - i] = byte.Parse(address.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return ba;
        }
    }
}
