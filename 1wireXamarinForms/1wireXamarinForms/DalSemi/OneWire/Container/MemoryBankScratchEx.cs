using _1wireXamarinForms.DalSemi.OneWire.Adapter;
using _1wireXamarinForms.DalSemi.OneWire.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Container
{

    /**
     * Memory bank class for the Scratchpad section of NVRAM iButtons and
     * 1-Wire devices.
     *
     *  @version    0.00, 28 Aug 2000
     *  @author     DS
     */


    public class MemoryBankScratchEx : MemoryBankScratch
    {



        //--------
        //-------- Constructor
        //--------

        /**
         * Memory bank contstuctor.  Requires reference to the OneWireContainer
         * this memory bank resides on.
         */
        public MemoryBankScratchEx(OneWireContainer ibutton)
            : base(ibutton)
        {

            // initialize attributes of this memory bank
            bankDescription = "Scratchpad Ex";

            // change copy scratchpad command
            COPY_SCRATCHPAD_COMMAND = (byte)0x5A;
        }

        //--------
        //-------- ScratchPad methods
        //--------

        /**
         * Write to the scratchpad page of memory a NVRAM device.
         *
         * @param  startAddr     starting address
         * @param  writeBuf      byte array containing data to write
         * @param  offset        offset into readBuf to place data
         * @param  len           length in bytes to write
         *
         * @throws OneWireIOException
         * @throws OneWireException
         */
        public override void WriteScratchpad(int startAddr, byte[] writeBuf, int offset,
                                     int len)
        {
            Boolean calcCRC = false;

            if (len > pageLength)
                throw new OneWireException("Write exceeds memory bank end");

            // select the device
            if (!ib.Adapter.SelectDevice(ib.Address))
            {
                ForceVerify();

                throw new OneWireIOException("Device select failed");
            }

            // build block to send
            byte[] raw_buf = new byte[pageLength + 5]; //[37];

            raw_buf[0] = WRITE_SCRATCHPAD_COMMAND;
            raw_buf[1] = (byte)(startAddr & 0xFF);
            raw_buf[2] = (byte)(((startAddr & 0xFFFF) >> 8) & 0xFF);

            Array.Copy(writeBuf, offset, raw_buf, 3, len);

            // check if full page (can utilize CRC)
            if (((startAddr + len) % pageLength) == 0)
            {
                Array.Copy(ffBlock, 0, raw_buf, len + 3, 2);

                calcCRC = true;
            }

            // send block, return result
            ib.Adapter.DataBlock(raw_buf, 0, len + 3 + ((calcCRC) ? 2
                                                                  : 0));
            //System.out.println("WriteScratchpad: " + com.dalsemi.onewire.utils.Convert.toHexString(raw_buf));

            // check crc
            if (calcCRC)
            {
                if (CRC16.Compute(raw_buf, 0, len + 5, 0) != 0x0000B001)
                {
                    ForceVerify();

                    throw new OneWireIOException("Invalid CRC16 read from device");
                }
            }
        }

        /**
         * Copy the scratchpad page to memory.
         *
         * @param  startAddr     starting address
         * @param  len           length in bytes that was written already
         *
         * @throws OneWireIOException
         * @throws OneWireException
         */
        public override void CopyScratchpad(int startAddr, int len)
        {

            // select the device
            if (!ib.Adapter.SelectDevice(ib.Address))
            {
                ForceVerify();

                throw new OneWireIOException("Device select failed");
            }

            // build block to send
            byte[] raw_buf = new byte[6];

            raw_buf[0] = COPY_SCRATCHPAD_COMMAND;
            raw_buf[1] = (byte)(startAddr & 0xFF);
            raw_buf[2] = (byte)(((startAddr & 0xFFFF) >> 8) & 0xFF);
            raw_buf[3] = (byte)((startAddr + len - 1) & 0x1F);

            Array.Copy(ffBlock, 0, raw_buf, 4, 2);

            // send block (check copy indication complete)
            ib.Adapter.DataBlock(raw_buf, 0, raw_buf.Length);

            if (((byte)(raw_buf[raw_buf.Length - 1] & 0x0F0) != (byte)0xA0)
                    && ((byte)(raw_buf[raw_buf.Length - 1] & 0x0F0)
                        != (byte)0x50))
            {
                ForceVerify();

                throw new OneWireIOException("Copy scratchpad complete not found");
            }
        }


    }
}
