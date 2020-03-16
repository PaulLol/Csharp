using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Other
{
    public class OtherMetods
    {
        private string MailAdmin = "TestMailService11@gmail.com";
        private string MailPass = "1234qwer890uio";
        public string RandomString()
        {
            int length = 7;

            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }

        public void SendMail(Temp temp)
        {
            var loginInfo = new NetworkCredential(MailAdmin, MailPass);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(MailAdmin);
            msg.To.Add(new MailAddress(temp.Mail));
            msg.Subject = "Password";
            msg.Body = "Register password: " + temp.Password;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}
