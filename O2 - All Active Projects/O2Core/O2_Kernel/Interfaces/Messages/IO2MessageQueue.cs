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