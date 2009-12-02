// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using DotNetGuru.AspectDNG.Joinpoints;
using O2.DotNetWrappers.Windows;

namespace O2.Rnd.AspectDngHook
{
    public class DngAspect
    {
        public static String logTargetDir = getTargetDir();

        #region Setup Log Environment        

        public static String getTargetDir()
        {
            String sTargetDir = Assembly.GetExecutingAssembly().Location + "_CallLogs";
            if (false == Directory.Exists(sTargetDir))
                Directory.CreateDirectory(sTargetDir);
            //Log("in DngAspect setting the target dir to: " + sTargetDir);
            return sTargetDir;
        }

        #endregion

        #region Log data

        private static int iLogCount;

        public static string Log(OperationJoinPoint ojpOperationJoinPoint, object oReturnData)
        {
            try
            {
                logTargetDir = DI.config.getTempFolderInTempDirectory("o2.AspectDngHook.dll_CallLogs");
                Files.checkIfDirectoryExistsAndCreateIfNot(logTargetDir);
                var sbLogMessage = new StringBuilder();
                sbLogMessage.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

                sbLogMessage.AppendLine("  <AspectDngTrace");

                sbLogMessage.AppendLine("      TargetOperationName=\"" + ojpOperationJoinPoint.TargetOperationName +
                                        "\" ");
                sbLogMessage.AppendLine("      TargetOperation=\"" + ojpOperationJoinPoint.TargetOperation + "\" >");

                sbLogMessage.AppendLine("    <Arguments>");
                foreach (Object oArgument in ojpOperationJoinPoint.Arguments)
                    sbLogMessage.AppendLine("        <Arguments type=\"" + oArgument.GetType().FullName + "\" value=\"" +
                                            oArgument + "\"/>");
                sbLogMessage.AppendLine("    </Arguments>");

                if (oReturnData == null)
                    sbLogMessage.AppendLine("    <ReturnData/>");
                else
                    sbLogMessage.AppendLine("    <ReturnData><![CDATA[ " + oReturnData + "    ]]></ReturnData>");


                sbLogMessage.AppendLine("  <Thread");
                sbLogMessage.AppendLine("    ManagedThreadId=\"" + Thread.CurrentThread.ManagedThreadId + "\"");
                sbLogMessage.AppendLine("    Name=\"" + Thread.CurrentThread.Name + "\"/>");

                sbLogMessage.AppendLine("    <StackTrace><![CDATA[");
                sbLogMessage.AppendLine(new StackTrace(false) + "    ]]></StackTrace>");
                sbLogMessage.AppendLine("  </AspectDngTrace>");


                return LogToFile(sbLogMessage.ToString());                
            }                        
            catch (Exception ex)
            {
                DI.log.ex(ex);
                return ""; 
            }
        }

        public static void Log(String sMessage)
        {
            //LogToSystemDiagnostics(sMessage);
            LogToFile(sMessage);
        }

        public static void LogToSystemDiagnostics(String sMessage)
        {
            Debug.WriteLine(sMessage);
        }

        public static void Log(Exception ex, OperationJoinPoint ojpOperationJoinPoint)
        {
        }

        public static String LogToFile(String message)
        {
            return LogToFile(message, ".xml");
        }

        public static String LogToFile(String message, string fileExtension)
        {
            try
            {
                String fileToSaveLog = Path.Combine(logTargetDir,
                                                     "LogEntry_ [" + iLogCount++ + "]_" + DateTime.Now.ToFileTime() +
                                                     fileExtension);
                File.WriteAllText(fileToSaveLog, message);
                return fileToSaveLog;
            }
            catch (Exception ex)
            {
                LogToSystemDiagnostics(ex.Message);
                return "";
            }
        }

        #endregion

        #region Aspects (at Joinpoints)

        [AroundCall("[[DISABLED]]")]
        public static object AroundCallFilter(OperationJoinPoint ojpOperationJoinPoint)
        {
            //System.Diagnostics.Debug.WriteLine("AroundCall:" + ojpOperationJoinPoint.ToString());
            //Log("AroundCall:" + ojpOperationJoinPoint.ToString());

            try
            {
                object oReturnValue = ojpOperationJoinPoint.Proceed();
                Log(ojpOperationJoinPoint, oReturnValue);
                //if (oReturnValue != null)
                //    Log(ojpOperationJoinPoint.TargetOperationName + " returned:" + oReturnValue);
                return oReturnValue;
            }
            catch (Exception ex)
            {
                Log(ex, ojpOperationJoinPoint);
                return null;
            }
        }

        [AroundBody("[[DISABLED]]")]
        public static object AroundBodyFilter(OperationJoinPoint ojpOperationJoinPoint)
        {
            //Log("AroundBody:::" + ojpOperationJoinPoint.ToString());
            //Log(ojpOperationJoinPoint);
            try
            {
                object oReturnValue = ojpOperationJoinPoint.Proceed();
                Log(ojpOperationJoinPoint, oReturnValue);
                //if (oReturnValue != null)
                //    Log(ojpOperationJoinPoint.TargetOperationName + " returned:" + oReturnValue);
                return oReturnValue;
            }
            catch (Exception ex)
            {
                Log(ex, ojpOperationJoinPoint);
                return null;
            }
        }

        #endregion

        //
    }
}
