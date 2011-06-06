// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using FluentSharp.O2.Interfaces.Messages;
using FluentSharp.O2.Kernel.CodeUtils;
 
//O2File:../CodeUtils/O2Kernel_O2Thread.cs
//O2File:../CodeUtils/Callbacks.cs
//O2File:KO2GenericMessage.cs
//O2File:KO2Log.cs

namespace FluentSharp.O2.Kernel.InterfacesBaseImpl
{
    public class KO2MessageQueue : IO2MessageQueue
    {
        private static readonly KO2MessageQueue o2MessageQueue = new KO2MessageQueue();

        private KO2MessageQueue()
        {
          //  onMessages += o2Message => DI.log.i("in KO2MessageQueue: message Received:" + o2Message.messageText);
        }

        public static KO2MessageQueue getO2KernelQueue()
        {
            return o2MessageQueue;
        }

        public event Action<IO2Message> onMessages;

        public Thread sendMessage(string messageText)
        {
            var o2Message = new KO2GenericMessage(messageText);
            return sendMessage(o2Message);
        }

        public Thread sendMessage(IO2Message messageToSend)
        {
            return Callbacks.raiseRegistedCallbacks(onMessages, new object[] { messageToSend });
        }

        public void sendMessageSync(IO2Message messageToSend)
        {
            var messageSent = new AutoResetEvent(false);
            O2Kernel_O2Thread.mtaThread((() =>
                                                        {
                                                            var messageThread = sendMessage(messageToSend);
                                                            messageThread.Join();
                                                            messageSent.Set();
                                                        }));
            messageSent.WaitOne();
        }

        //public KO2MessageQueue()
        //{
        //    
        //                  
        //}
    }
}
