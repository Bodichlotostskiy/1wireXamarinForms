using _1wireXamarinForms.DalSemi.OneWire.Adapter;
using _1wireXamarinForms.DalSemi.OneWire.Container;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.CustomContainer
{
    /// <summary>
    /// The CustomOneWireContainer acts as an abstract base class for all custom OneWire devices, i.e. devices not manufactured my Dallas.
    /// An example on such a device is Louis Swart's LCD controller. This class exists only to allow the OW.Net library to distinguish between
    /// the standard devices and the custom ones included in the OW.Net library.
    /// 
    /// All custom containers are put on their own list for retrieval by the various methods in the PortAdapter class.
    /// Since custom devices doesn't follow the rule of a unique family code (generally 0xFF is used to mark a device as
    /// a custom type) for each type, they cannot be automatically created when found on the network. To come around this
    /// problem, the configuration (OneWire.properties) file is read for a list of ROM codes. If a matching ROM code is
    /// found in the file, then an instance of the class specified for that ROM code is created. If no match is found, a
    /// OneWireContainer will be used to encapsulate the device.
    /// This can also be used as a way to replace a standard container with a custom one for a specific device on the network.
    /// 
    /// The correct syntax in the OneWire.properties file is:
    /// CustomDevice.ROM=ClassName
    /// 
    /// Example:
    /// CustomDevice.C500010000039BFF=MyClassName
    /// </summary>
    public abstract class CustomOneWireContainer : OneWireContainer
    {
        public CustomOneWireContainer(PortAdapter portAdapter, byte[] address) : base(portAdapter, address)
        {
        }
    }
}
