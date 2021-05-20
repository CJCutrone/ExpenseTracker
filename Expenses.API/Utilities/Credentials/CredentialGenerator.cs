using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Lookups.Authentication.Credentials;
using System;

namespace Expenses.API.Utilities.Credentials
{
    public static class CredentialGenerator
    {
        public static Credential GenerateTokenCredential(Guid token)
        {
            var now = DateTimeOffset.UtcNow;

            var saltBytes = SaltGenerator.Generate(32);
            var hashedBytes = PlainTextHasher.Hash(token.ToByteArray(), saltBytes);

            var credential = new Credential();
            credential.Salt = Convert.ToBase64String(saltBytes);
            credential.Hash = Convert.ToBase64String(hashedBytes);
            credential.CreatedOn = now;
            credential.ExpiresOn = now.AddMinutes(15);
            credential.Type = new CredentialType(CredentialTypes.ResetToken);

            return credential;
        }
    }
}
