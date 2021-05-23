using Expenses.API.Properties;

namespace Expenses.API.Utilities.Email
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public static EmailMessage ResetPassword(string url)
        {
            var message = new EmailMessage();
            message.Subject = "Password Reset Request";
            message.Body = Resources
                            .resetPassword
                            .Replace("{{resetPassword}}", url)
                            ;

            return message;
        }


        public static class Type
        {
            public static readonly string Reset = Resources.resetPassword;
        }
    }
}