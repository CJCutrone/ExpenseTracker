using Dapper;
using Expenses.Data.Model.Authentication.Users;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Queries.Authentication
{
    public class GetUserByEmailQuery
        : IExpenseQuery<User, User>
    {
        public async Task<User> OnExecute(DbConnection connection, User user)
        {
            var response = (await connection
                .QueryAsync<UserRow>(GetUserByEmail, new { email = user.Email }))
                .Single();

            return (User) response;
        }

        private class UserRow
        {
            private Guid Id { get; }
            private string Username { get; }
            private string Email { get; }

            private Guid RoleId { get; }
            private string RoleCode { get; }

            public static explicit operator User(UserRow row)
            {
                var user = new User();
                user.Id = row.Id;
                user.Username = row.Username;
                user.Email = row.Email;

                user.Role = new UserRole();
                user.Role.Id = row.RoleId;
                user.Role.Code = row.RoleCode;

                return user;
            }
        }

        #region GetUserByEmailQuery
        public const string GetUserByEmail =
@"
SELECT 
    u.[Id],
    u.[Username],
    u.[Email],
    r.[Id] as [RoleId]
    r.[Code] as [RoleCode]
FROM [Expenses].[User] u
INNER JOIN [Expenses].[Role] r
    ON r.[Id] = u.[RoleId]
WHERE users.[Email] = @email;
";
        #endregion
    }
}
