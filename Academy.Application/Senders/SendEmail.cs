using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Academy.Application.Senders
{
    public class SendEmail
    {
        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("webmail.iaminmoradi.ir");
            mail.From = new MailAddress("noreply@iaminmoradi.ir", "coinglas/ارز دیجیتال");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreply@iaminmoradi.ir", "********");
            SmtpServer.EnableSsl = true;
            

            SmtpServer.Send(mail);

        }
    }
}