using Dapper;
using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Users;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Queries.Authentication.Credentials
{
    public class InsertNewCredentialQuery
        : IExpenseQuery<Credential, NoResponse>
    {
        public async Task<NoResponse> OnExecute(DbConnection connection, Credential credential)
        {
            var response = (await connection
                .ExecuteAsync(
                    GetLoginMethod,
                    new { 
                        userId = credential.User.Id,
                        createdByUserId = credential.CreatedBy.Id,
                        credentialType = credential.Type.Code,
                        salt = credential.Salt,
                        hash = credential.Hash,
                        expiresOn = credential.ExpiresOn
                    }
                ));

            //check for credential within the last 15 minutes

            if (response != 1)
                throw new Exception("Failed to insert credential into database");

            return default;
        }

        #region GetLoginMethod
        public const string GetLoginMethod =
@"
INSERT INTO [Expenses].[Credential] (
    [UserId], 
    [CreatedByUserId],
	[CredentialTypeId],
	[Salt],
	[Hash],
	[ExpiresOn]
)
SELECT
    @userId,
    @createdByUserId,
    ct.[Id],
    @salt,
    @hash,
    @expiresOn
FROM [Expenses].[Credential.Type] ct
WHERE ct.[Code] = @credentialType;
";
        #endregion
    }
}
