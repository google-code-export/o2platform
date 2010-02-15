// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Threading;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.Interfaces.Messages
{
    public interface IO2MessageQueue
    {
        event Callbacks.callbackFor_O2Message onMessages;

        Thread sendMessage(string messageText);
        Thread sendMessage(IO2Message messageToSend);

        void sendMessageSync(IO2Message messageToSend);
    }
}
