using Expenses.Data.Model.Authentication.Users;
using System;

namespace Expenses.Data.Model.Authentication.Credentials
{
    public class Credential
    {
        public User User { get; set; }
        public User CreatedBy { get; set; }

        public CredentialType Type { get; set; }

        public string Salt { get; set; }
        public string Hash { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
    }
}
