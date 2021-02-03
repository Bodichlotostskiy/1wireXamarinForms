using _1wireXamarinForms.DalSemi.OneWire.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.CustomAdapter
{
    internal class HomebrewSerialAdapter: TMEXLibAdapter
    {
        #region Constructors and Destructors
        /// <summary>
        /// Constructs a HomebrewSerialAdapter
        /// </summary>
        /// <throws>ClassNotFoundException</throws>
        public HomebrewSerialAdapter()
        {
            // attempt to set the portType, will throw exception if does not exist
            if (!SetTMEXPortType(TMEXPortType.PassiveSerialPort))
            {
                throw new AdapterException("TMEX adapter type does not exist");
            }
        }

        /// <summary>
        /// Constructs an instance of the HomebrewSerialAdapter
        /// </summary>
        /// <param name="">newPortType</param>
        /// <throws>  ClassNotFoundException </throws>
        public HomebrewSerialAdapter(TMEXPortType newPortType)
        {
            // attempt to set the portType, will throw exception if does not exist
            if (newPortType != TMEXPortType.PassiveSerialPort)
            {
                throw new AdapterException("The HomebrewSerialAdapter can only fake a passive serial adapter type");
            }
            if (!SetTMEXPortType(TMEXPortType.PassiveSerialPort))
            {
                throw new AdapterException("TMEX adapter type does not exist");
            }
        }
        #endregion // Constructors and Destructors

        #region Power Delivery

        /// <summary>
        /// Fakes setting the 1-Wire Network voltage to supply power to an iButton device.
        /// </summary>
        public override bool StartPowerDelivery(OWPowerStart changeCondition)
        {
            return true;
        }


        /// <summary>
        /// Fakes setting the power back to normal 
        /// </summary>
        public override void SetPowerNormal()
        {

        }

        /// <summary>
        /// Fakes power capability
        /// </summary>
        /// <value></value>
        /// <returns>
        /// true always
        /// </returns>
        public override bool CanDeliverPower
        {
            get { return true; }
        }

        #endregion
    }
}
