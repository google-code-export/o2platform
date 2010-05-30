using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime; 
//Mime is Not necerrary if you dont change the msgview and 
//if you dont add custom/extra headers 
using System.Threading;

namespace DemoApp
{
    class EmailException
    {
        public static string EMAILUSER = "watinrecorder@gmail.com";
        private static string EMAILPASS = "w@t1n1";

        public void SendMail(string Body, string Subject, string FromEmail, bool CopyUser, bool Async)
        {
            //Build The MSG
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(EMAILUSER);
            msg.Bcc.Add("daarond@gmail.com");

            if (CopyUser)
            {
                msg.CC.Add(FromEmail);
            }
            if (FromEmail == "")
            {
                FromEmail = EMAILUSER;
            }
            msg.From = new MailAddress(FromEmail);
            msg.Subject = "Recorder " + Application.ProductVersion + " "+Subject+" " + Environment.MachineName + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            msg.Body = "From: "+FromEmail+Environment.NewLine+Body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            //Add the Creddentials
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(EMAILUSER, EMAILPASS);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            object userState = msg;

            try
            {
                if (Async)
                {
                    client.SendAsync(msg, userState);
                }
                else
                {
                    client.Send(msg);
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.SendMailError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendMail(System.Exception exc, string FromEmail, string Comment, bool CopyUser, bool Async)
        {
            System.Text.StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("Exception:");
            sbBody.AppendLine(exc.Message);
            sbBody.AppendLine("");

            if (Comment.Length > 0)
            {
                sbBody.AppendLine("Reproduction:");
                sbBody.AppendLine(Comment);
                sbBody.AppendLine("");
            }
            
            sbBody.AppendLine("Stack Trace:");
            sbBody.AppendLine(exc.StackTrace);

            SendMail(sbBody.ToString(), "Bug", FromEmail, CopyUser, Async);
        }

        void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                string cancelled = string.Format(Properties.Resources.SendCanceled, subject);
                MessageBox.Show(cancelled);                
            }
            if (e.Error != null)
            {
                string error = string.Format("[{0}] {1}", subject, e.Error.ToString());
                MessageBox.Show(error);                
            }
            else
            {
                MessageBox.Show(Properties.Resources.MessageSentSuccessfully);
            }
        }
    }
}
