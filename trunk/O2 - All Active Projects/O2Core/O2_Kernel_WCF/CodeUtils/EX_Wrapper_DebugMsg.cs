// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.Kernel.Objects;

namespace O2.Kernel.CodeUtils
{
    public static class EX_Wrapper_DebugMsg
    {
        public static DialogResult showMessageBox(this O2AppDomainFactory o2AppDomainFactory, string message)
        {
            return showMessageBox(o2AppDomainFactory, message, "Message from O2 AppDomain Central", MessageBoxButtons.OK);
        }


        public static DialogResult showMessageBox(this O2AppDomainFactory o2AppDomainFactory, string message,
                                                  string messageBoxTitle, MessageBoxButtons messageBoxButtons)
        {
            return (DialogResult) o2AppDomainFactory.proxyInvokeStatic("O2_Kernel", "O2Proxy", "showMessageBox",
                                                                       new object[]
                                                                           {message, messageBoxTitle, messageBoxButtons});
        }

        public static void logInfo(this O2AppDomainFactory o2AppDomainFactory, string infoMessage)
        {
            o2AppDomainFactory.proxyInvokeStatic("O2_Kernel", "O2Proxy", "logInfo", new object[] { infoMessage });
        }

        public static void logDebug(this O2AppDomainFactory o2AppDomainFactory, string debugMessage)
        {
            o2AppDomainFactory.proxyInvokeStatic("O2_Kernel", "O2Proxy", "logDebug", new object[] { debugMessage });
        }

        public static void logError(this O2AppDomainFactory o2AppDomainFactory, string errorMessage)
        {
            o2AppDomainFactory.proxyInvokeStatic("O2_Kernel", "O2Proxy", "logError", new object[] { errorMessage });
        }
    }
}
