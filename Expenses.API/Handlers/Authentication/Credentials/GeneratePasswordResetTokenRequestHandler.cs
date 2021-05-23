using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.API.Utilities.Credentials;
using Expenses.API.Utilities.Credentials.Configuration;
using Expenses.API.Utilities.Email;
using Expenses.Data.Access.Database;
using Expenses.Data.Access.Queries.Authentication;
using Expenses.Data.Access.Queries.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Lookups.Authentication.Credentials;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.API.Handlers.Authentication.Credentials
{
    public class GeneratePasswordResetTokenRequestHandler
        : IRequestHandler<GeneratePasswordResetTokenRequest, GeneratePasswordResetResponse>
    {
        private readonly IExpenseDatabase Database;
        private readonly IOptionsSnapshot<EmailCredentials> credentials;

        public GeneratePasswordResetTokenRequestHandler(
            IExpenseDatabase database,
            IOptionsSnapshot<EmailCredentials> credentials
        )
        {
            this.Database = database;
            this.credentials = credentials;
        }

        public async Task<GeneratePasswordResetResponse> Handle(GeneratePasswordResetTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await this.Database.PrepareAndExecute(new GetUserByEmailQuery(), request.User);
            if (user.Id == null)
            {
                // no user was found by that email, but we don't want the user to know that. The process still completes,
                // so it is a success, we just stop here.
                return new GeneratePasswordResetResponse() { IsSuccess = true };
            }

            var token = Guid.NewGuid();
            var credential = CredentialGenerator.GenerateTokenCredential(token);
            credential.User = user;
            credential.CreatedBy = user;

            await this.Database.PrepareAndExecute(new InsertNewCredentialQuery(), credential);

            // send email to user
            var message = EmailMessage.ResetPassword($"/reset/{token}");
            Email.SendTo(this.credentials.Value, message, user);

            return new GeneratePasswordResetResponse() { IsSuccess = true };
        }
    }
}
