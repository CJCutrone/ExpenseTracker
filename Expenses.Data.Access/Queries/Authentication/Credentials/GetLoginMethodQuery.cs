using Dapper;
using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Users;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Queries.Authentication.Credentials
{
    public class GetLoginMethodQuery
        : IExpenseQuery<User, List<CredentialType>>
    {
        public async Task<List<CredentialType>> OnExecute(DbConnection connection, User user)
        {
            var response = (await connection
                .QueryAsync<CredentialType>(GetLoginMethod, new { username = user.Username }))
                .ToList();

            return response;
        }

        #region GetLoginMethod
        public const string GetLoginMethod =
@"
SELECT 
	ct.[Id],
	ct.[Name],
	ct.[Code],
	ct.[Description]
FROM [Expenses].[MostRecentCredentialTypes] latest
INNER JOIN [Expenses].[Credential.Type] ct
    ON ct.[Id] = latest.[CredentialTypeId]
INNER JOIN [Expenses].[User] users
	ON users.[Id] = latest.[UserId]
WHERE users.[Username] = @username;
";
        #endregion
    }
}
