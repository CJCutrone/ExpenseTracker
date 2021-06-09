using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.API.Utilities.Credentials;
using Expenses.API.Utilities.Credentials.Configuration;
using Expenses.API.Utilities.Email;
using Expenses.Data.Access.Database;
using Expenses.Data.Access.Queries.Authentication;
using Expenses.Data.Access.Queries.Authentication.Credentials;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.API.Handlers.Authentication.Credentials
{
    public class GeneratePasswordResetTokenRequestHandler
        : IRequestHandler<GeneratePasswordResetTokenRequest, GeneratePasswordResetResponse>
    {
        private readonly IExpenseDatabase Database;
        private readonly IOptionsSnapshot<EmailCredentials> credentials;
        private readonly HttpContext context;

        public GeneratePasswordResetTokenRequestHandler(
            IExpenseDatabase database,
            IHttpContextAccessor context,
            IOptionsSnapshot<EmailCredentials> credentials
        )
        {
            this.Database = database;
            this.credentials = credentials;
            this.context = context.HttpContext;
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
            var message = EmailMessage.ResetPassword($"{context.Request.Host}/reset/{token}");
            Email.SendTo(this.credentials.Value, message, user);

            return new GeneratePasswordResetResponse() { IsSuccess = true };
        }
    }
}
