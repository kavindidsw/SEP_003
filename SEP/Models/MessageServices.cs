using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace SEP.Models
{
    public class MessageServices
    {
        public async static Task SendEmailAsync(string email,string subject,string message)
        {

            try
            {
                var _email = "dondralibrary@gmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "HASH";
                MailMessage mymessage = new MailMessage();
                mymessage.To.Add(email);
                mymessage.From = new MailAddress(_email, _epass);
                mymessage.Subject = subject;
                mymessage.Body = message;
                mymessage.IsBodyHtml = true;

                using(SmtpClient smtp=new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(mymessage);
                 
                }

            }
            catch(Exception ex)
            {
                throw ex;

            }
        }

        internal static void SendEmailAsync(string v1, string v2, Task<string> message)
        {
            throw new NotImplementedException();
        }
    }
}