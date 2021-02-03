using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Adapter
{
    /// <summary>
    /// Constructs a <code>OneWireIOException</code> with the specified detail message.
    /// </summary>
    /// <param name="desc">The detail message description</param>
    public class OneWireIOException : OneWireException
    {
        public OneWireIOException(string desc)
            : base(desc)
        {
        }

    }
}
