// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Windows.Forms;
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.Kernel.CodeUtils;

namespace FluentSharp.O2.DotNetWrappers.Network
{
    public class Mail
    {
        #region Delegates

        public delegate void dPingCompleted(Object oObject, AsyncCompletedEventArgs e);

        #endregion

        public static event Callbacks.dMethod_Bool eventEmailCompleted;

        public static bool isMailServerOnline(string mailServerToTest)
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.Connect(mailServerToTest, 25);
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in isMailServerOnline :{0}", ex.Message);
            }
            return false;
        }

        public static bool sendMail(String sHost, String sFrom, String sTo, String sCC, String sSubject, String sMessage,
                                    List<String> lsAttachmentFiles, bool bSendSync)
        {
            return sendMail(sHost, sFrom, sTo, sCC, sSubject, sMessage, lsAttachmentFiles, bSendSync, null);
        }

        public static bool sendMail(String sHost, String sFrom, String sTo, String sCC, String sSubject, String sMessage,
                                    List<String> lsAttachmentFiles, bool bSendSync, Callbacks.dMethod_Bool callback)
        {
            try
            {
                DI.log.info("Sending email:\n" +
                            "/t/thost:{0}\n" +
                            "/t/tfrom:{1}\n" +
                            "/t/tto:{2}\n" +
                            "/t/tcc:{3}\n" +
                            "/t/tSubject:{4}\n" +
                            "/t/t# attachments:{5}",
                            sHost, sFrom, sTo, sFrom, sCC, sSubject, lsAttachmentFiles.Count);
                var mmMailMessage = new MailMessage(new MailAddress(sFrom), new MailAddress(sTo))
                                        {From = new MailAddress(sFrom)};
                if (sCC != "")
                    mmMailMessage.CC.Add(new MailAddress(sCC));
                mmMailMessage.Subject = sSubject;
                mmMailMessage.Body = sMessage;
                foreach (String sAttachment in lsAttachmentFiles)
                {
                    mmMailMessage.Attachments.Add(new Attachment(sAttachment));
                }
                var scSmtpClient = new SmtpClient {Host = sHost};
                scSmtpClient.SendCompleted += scSmtpClient_SendCompleted;
                if (bSendSync)
                {
                    if (callback != null)
                        eventEmailCompleted += callback;
                    scSmtpClient.SendAsync(mmMailMessage, mmMailMessage.Subject);
                }
                else
                    scSmtpClient.Send(mmMailMessage);
            }
            catch (Exception ex)
            {
                DI.log.error("In sendMail: {0}", ex.Message);
                callback(false);
                return false;
            }
            return true;
        }

        private static void scSmtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var sMessageToken = (string) e.UserState;
            bool result = false;
            if (e.Cancelled)
                DI.log.error("Send message canceled: {0}", sMessageToken);
            else if (e.Error != null)
                DI.log.error("Error in sending message '{0}' : {1}", sMessageToken, e.Error.ToString());
            else
            {
                DI.log.info("Message sent: {0}", sMessageToken);
                result = true;
            }

            if (eventEmailCompleted != null)
                foreach (Delegate dDelegate in eventEmailCompleted.GetInvocationList())
                    dDelegate.DynamicInvoke(result);
        }

        // 
        public static List<String> createAttachmentsForRemoteSupport(RichTextBox richTextBoxWithLog,
                                                                     PictureBox screenShotToSend, string sFile_LogViews,
                                                                     string sFile_LogViewsTxt, string sFile_ScreenShot)
        {
            var lsAttachements = new List<string>();
            if (richTextBoxWithLog != null && richTextBoxWithLog.Text != "")
            {
                Files.WriteFileContent(sFile_LogViews, richTextBoxWithLog.Rtf);
                lsAttachements.Add(sFile_LogViews);
                Files.WriteFileContent(sFile_LogViewsTxt, richTextBoxWithLog.Text);
                lsAttachements.Add(sFile_LogViewsTxt);
            }

            if (screenShotToSend.BackgroundImage != null)
            {
                screenShotToSend.BackgroundImage.Save(sFile_ScreenShot, ImageFormat.Jpeg);
                lsAttachements.Add(sFile_ScreenShot);
            }
            return lsAttachements;
        }

        public static String getUserDetailsAsEmailFormat()
        {
            try
            {
                return string.Format("{0}.{1}@{2}", Environment.UserName, Environment.MachineName, Dns.GetHostName());
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getUserDetailsAsEmailFormat");
                return "o2User@o2-Ounceopen.com";
            }
        }
    }
}
