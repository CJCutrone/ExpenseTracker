using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.Data.Model.Authentication.Users;
using MediatR;

namespace Expenses.API.Controllers.Authentication.Credentials.Request
{
    public class GeneratePasswordResetTokenRequest
        : IRequest<GeneratePasswordResetResponse>
    {
        public readonly User User;

        public GeneratePasswordResetTokenRequest(User user)
        {
            this.User = user;
        }
    }
}
