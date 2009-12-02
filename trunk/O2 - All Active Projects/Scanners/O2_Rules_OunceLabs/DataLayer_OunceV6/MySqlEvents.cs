using System;
using O2.Kernel.CodeUtils;
using O2.Kernel.CodeUtils;
using O2.Rules.OunceLabs.DataLayer;

namespace O2.Rules.OunceLabs.DataLayer
{
    public class MySqlEvents
    {
        public static event Callbacks.dMethod_String_String eShowCustomRulesDetails_MethodSignature;

        public static void raiseEvent_ShowCustomRulesDetails_MethodSignature(String sActionObjectId)
        {
            UInt32 uActionObjectId = 0;
            if (UInt32.TryParse(sActionObjectId, out uActionObjectId))
            {
                String sMethodDbId = Lddb_OunceV6.action_getDbIDFromActionObjectId(uActionObjectId).ToString();
                String sMethodSignature = Lddb_OunceV6.action_getMethodSignatureFromActionObjectId(uActionObjectId);
                raiseEvent_ShowCustomRulesDetails_MethodSignature(sMethodDbId, sMethodSignature);
            }
        }

        public static void raiseEvent_ShowCustomRulesDetails_MethodSignature(String sMethodDbId, String sMethodSignature)
        {
            if (eShowCustomRulesDetails_MethodSignature != null)
            {
                Delegate[] dInvocationList = eShowCustomRulesDetails_MethodSignature.GetInvocationList();
                foreach (Delegate dDelegate in dInvocationList)
                    dDelegate.DynamicInvoke(new Object[] {sMethodDbId, sMethodSignature});
            }
        }
    }
}