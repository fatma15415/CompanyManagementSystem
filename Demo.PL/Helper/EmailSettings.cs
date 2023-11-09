using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmails(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("gfatma238@gmail.com", "Fatma");
            Client.Send("gfatma238@gmail.com", email.Recipients, email.Subject, email.Body);
        }

    }
}
