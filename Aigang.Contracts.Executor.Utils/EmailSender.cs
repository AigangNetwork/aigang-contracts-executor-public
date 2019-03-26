using System.Net;
using System.Net.Mail;
using Aigang.Contracts.Executor.Utils;

namespace Aigang.Transactions.Utils
{
    public static class EmailSender
    {
        private static string _host = ConfigurationManager.GetString("Email:SMTPHost");
        private static int _port = ConfigurationManager.GetInt("Email:SMTPPort");
        private static string _user = ConfigurationManager.GetString("Email:SMTPUser");
        private static string _password = ConfigurationManager.GetString("Email:SMTPPassword");
        private static string _from = ConfigurationManager.GetString("Email:SMTPFromEmail");

        private static string _team = ConfigurationManager.GetString("Email:Team");
        private static string _appName = ConfigurationManager.GetString("AppName");


        private static void SendMail(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(to);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            SendMailMessage(mailMessage);
        }

        private static void SendMailMessage(MailMessage message)
        {
            message.From = new MailAddress(_from);

            if(!string.IsNullOrWhiteSpace(_host))
            {
                using (var client = new SmtpClient(_host, _port)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_user, _password)
                })
                {
                    client.Send(message);
                }
            }
        }
        
        public static void SendOutOfBalanceEmail(string subject, string message )
        {
            var body = message;
            SendMail(_team, $"[{_appName}] {subject}", body);
        }

    }
}