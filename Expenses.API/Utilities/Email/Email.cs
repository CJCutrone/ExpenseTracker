using Expenses.API.Utilities.Credentials.Configuration;
using Expenses.Data.Model.Authentication.Users;
using System.Net;
using System.Net.Mail;

namespace Expenses.API.Utilities.Email
{
    public static class Email
    {
        public static void SendTo(this EmailCredentials credentials, EmailMessage email, User recipient)
        {
            var mySmtpClient = new SmtpClient(credentials.Client);
            var basicAuthenticationInfo = new NetworkCredential(credentials.Username, credentials.Password);

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            var from = new MailAddress(credentials.Username, "Expenses");
            var to = new MailAddress(recipient.Email, recipient.Username);
            var myMail = new MailMessage(from, to);

            // set subject and encoding
            myMail.Subject = email.Subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = email.Body;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;

            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
        }
    }
}
