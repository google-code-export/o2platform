using System;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces.Messages
{
    public interface IO2Message
    {        
        Guid messageGUID { get; set; }
        string messageText { get; set; }
        object returnData { get; set; }
    }
}