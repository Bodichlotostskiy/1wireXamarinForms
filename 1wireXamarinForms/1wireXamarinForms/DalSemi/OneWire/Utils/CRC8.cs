﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Utils
{

    /// <summary>
    /// CRC8 is a class to contain an implementation of the
    /// Cyclic-Redundency-Check CRC8 for the iButton.  The CRC8 is used
    /// in the 1-Wire Network address of all iButtons and 1-Wire devices.
    /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
    /// </summary>
    public class CRC8
    {

        //--------
        //-------- Variables
        //--------


        /// <summary>
        /// CRC 8 lookup table
        /// </summary>
        private static byte[] dscrc_table;

        //--------
        //-------- Constructor
        //--------

        /// <summary> Private constructor to prevent instantiation.</summary>
        private CRC8()
        {
        }

        //--------
        //-------- Methods
        //--------

        /// <summary>
        /// Perform the CRC8 on the data element based on the provided seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">data element on which to perform the CRC8</param>
        /// <param name="seed">seed the CRC8 with this value</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(uint dataToCRC, uint seed)
        {
            return (uint)(dscrc_table[(seed ^ dataToCRC) & 0x0FF] & 0x0FF);
        }

        /// <summary>
        /// Perform the CRC8 on the data element based on a zero seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">data element on which to perform the CRC8</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(uint dataToCRC)
        {
            return (uint)(dscrc_table[dataToCRC & 0x0FF] & 0x0FF);
        }

        /// <summary>
        /// Perform the CRC8 on an array of data elements based on a zero seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">array of data elements on which to perform the CRC8</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(byte[] dataToCrc)
        {
            return (uint)Compute(dataToCrc, 0, dataToCrc.Length);
        }

        /// <summary>
        /// Perform the CRC8 on an array of data elements based on a zero seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">array of data elements on which to perform the CRC8</param>
        /// <param name="off">offset into array</param>
        /// <param name="len">length of data to crc</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(byte[] dataToCrc, int off, int len)
        {
            return (uint)Compute(dataToCrc, off, len, 0);
        }

        /// <summary>
        /// Perform the CRC8 on an array of data elements based on the provided seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">array of data elements on which to perform the CRC8</param>
        /// <param name="off">offset into array</param>
        /// <param name="len">length of data to crc</param>
        /// <param name="seed">seed to use for CRC8</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(byte[] dataToCrc, int off, int len, uint seed)
        {
            // loop to do the crc on each data element
            uint CRC8 = seed;

            for (int i = 0; i < len; i++)
                CRC8 = dscrc_table[(CRC8 ^ dataToCrc[i + off]) & 0x0FF];

            return (CRC8 & 0x0FF);
        }

        /// <summary>
        /// Perform the CRC8 on an array of data elements based on the provided seed.
        /// CRC8 is based on the polynomial = X^8 + X^5 + X^4 + 1.
        /// </summary>
        /// <param name="dataToCrc">array of data elements on which to perform the CRC8</param>
        /// <param name="seed">seed to use for CRC8</param>
        /// <returns>CRC8 value</returns>
        public static uint Compute(byte[] dataToCrc, uint seed)
        {
            return Compute(dataToCrc, 0, dataToCrc.Length, seed);
        }

        /// <summary>
        /// Initializes the <see cref="CRC8"/> class.
        /// </summary>
        static CRC8()
        {
            /*
            * Create the lookup table
            */

            //Translated from the assembly code in iButton Standards, page 129.
            dscrc_table = new byte[256];

            int acc;
            int crc;

            for (int i = 0; i < 256; i++)
            {
                acc = i;
                crc = 0;

                for (int j = 0; j < 8; j++)
                {
                    if (((acc ^ crc) & 0x01) == 0x01)
                    {
                        crc = ((crc ^ 0x18) >> 1) | 0x80;
                    }
                    else
                        crc = crc >> 1;

                    acc = acc >> 1;
                }

                dscrc_table[i] = (byte)crc;
            }
        }

    }
}
