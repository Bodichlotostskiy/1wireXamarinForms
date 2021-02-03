using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire
{
    public class OneWireException : Exception
    {
        /// <summary>
        /// Constructs a <code>OneWireException</code> with the specified detail message.
        /// </summary>
        /// <param name="desc">The detail message description</param>
        public OneWireException(string desc)
            : base(desc)
        {
        }
    }
}
