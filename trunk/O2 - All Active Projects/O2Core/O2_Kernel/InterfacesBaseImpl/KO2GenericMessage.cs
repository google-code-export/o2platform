using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.Messages;

namespace O2.Kernel.InterfacesBaseImpl
{
    public class KO2GenericMessage : KO2Message, IO2Message
    {                
        public string messageSugestedTarget { get; set; }
        public IO2Message inResponseToMessage { get; set; }
        public List<object> messageData { get; set; }
        public bool handled { get; set; }

        public KO2GenericMessage()
        {                   
            messageText = "";
            messageSugestedTarget = "";
            inResponseToMessage = null;
            messageData = new List<object>();
            handled = false;
        }

        public KO2GenericMessage(string _messageText) : this ()
        {
            messageText = _messageText;
        }
        public KO2GenericMessage(string _messageText, List<object> _messageData)
            : this()
        {
            messageText = _messageText;            
            messageData = _messageData;
        }
    }
}