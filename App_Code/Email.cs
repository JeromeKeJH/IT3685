using System;
using System.Net.Mail;

namespace IT3685.App_Code
{
    public class Email
    {
        public static bool SendEmail(string email, string subject, string body)
        {
            string from = "it3685bip@gmail.com";
            MailMessage message = new MailMessage();

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            message.From = new MailAddress(from, "IT3685BIP");
            message.To.Add(new MailAddress(email));

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential(from, "7AHc!@h9");

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}