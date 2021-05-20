using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.Data.Access.Database;
using Expenses.Data.Access.Queries.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.API.Handlers.Authentication.Credentials
{
    public class GeneratePasswordResetTokenRequestHandler
        : IRequestHandler<GeneratePasswordResetRequest, GeneratePasswordResetResponse>
    {
        public IExpenseDatabase Database;

        public GeneratePasswordResetTokenRequestHandler(IExpenseDatabase database)
        {
            this.Database = database;
        }

        public async Task<GeneratePasswordResetResponse> Handle(GeneratePasswordResetRequest request, CancellationToken cancellationToken)
        {
            var user = await this.Database.PrepareAndExecute(new GetUserByEmailQuery(), request.User);
            if (user.Id == null)
            {
                // no user was found by that email, but we don't want the user to know that. The process still completes,
                // so it is a success, we just stop here.
                return new GeneratePasswordResetResponse() { IsSuccess = true };
            }



            // generate token

            // send email to user

            return new GeneratePasswordResetResponse() { IsSuccess = true };
        }
    }
}
