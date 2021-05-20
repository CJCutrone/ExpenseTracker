using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.Data.Model.Authentication.Users;
using MediatR;

namespace Expenses.API.Controllers.Authentication.Credentials.Request
{
    public class GeneratePasswordResetRequest
        : IRequest<GeneratePasswordResetResponse>
    {
        public readonly User User;

        public GeneratePasswordResetRequest(User user)
        {
            this.User = user;
        }
    }
}
