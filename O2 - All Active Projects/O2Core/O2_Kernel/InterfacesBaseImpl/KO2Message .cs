using System;
using System.Reflection;
using O2.Kernel.Interfaces.Messages;

namespace O2.Kernel.InterfacesBaseImpl
{
    public class KO2Message : IO2Message
    {
        public Guid messageGUID { get; set; }
        public string messageText { get; set; }
        public object returnData { get; set; }        

        public KO2Message()
        {
            messageGUID = new Guid();
            messageText = "KO2Message";            
        }

    }
}