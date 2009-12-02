using System;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Rnd.AspectDngHook;


namespace O2.Rnd.AspectDngHook
{
    public class DngUtils
    {
        //public static String sAspectDngExecutable = Path.Combine(DngConfig.sCurrentExecutableDirectory,"AspectDng.exe");

        public static String injectHooks(String sTargetAssembly, String sInjectionCriteria_AroundCall,
                                         String sInjectionCriteria_AroundBody)
        {
            checkForBackupAndRestoreIfExists(sTargetAssembly);
            String sAspectDgnExe = DngConfig.extractAspectDngExeToTempFolder();
            String sCecilDll = DngConfig.extractCecilDllToTempFolder();

            //setup hooks
            String sHooks = DngConfig.copyCurrentDllToTempFolder();
            setDngInjectCriteria(sHooks, "AroundCall", sInjectionCriteria_AroundCall);
            setDngInjectCriteria(sHooks, "AroundBody", sInjectionCriteria_AroundBody);


            String sAspectDngExecutionParameters = String.Format("\"{0}\" \"{1}\"", sTargetAssembly, sHooks);
            String sConsoleOut = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(sAspectDgnExe,
                                                                                                  sAspectDngExecutionParameters);
            return sConsoleOut;
        }

        public static void setDngInjectCriteria(string sTargetAssembly, string sTargetAttribute, string sInjectCriteria)
        {
            if (sInjectCriteria != "")
                CecilUtils.setAttributeValueFromAssembly(sTargetAssembly, sTargetAttribute, 0, sInjectCriteria);
        }

        public static void checkForBackupAndRestoreIfExists(String sTargetAssembly)
        {
            String sBackupFile = sTargetAssembly + ".Backup";
            if (File.Exists(sBackupFile))
            {
                File.Delete(sBackupFile);
                File.Copy(sTargetAssembly, sBackupFile);
            }
        }
    }
}