using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace QuanLiKOL.Models.Common
{
    public class SendGmail
    {
        public void send(string nguoinhan, string sub, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("meliodas200620011505@gmail.com");
            msg.To.Add(nguoinhan);
            msg.Subject = sub;
            msg.Body = body;
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("meliodas200620011505@gmail.com", "yvakdcldlmewwdej");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
        }


    }
}