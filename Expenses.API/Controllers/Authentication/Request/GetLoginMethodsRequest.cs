using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Users;
using MediatR;
using System.Collections.Generic;

namespace Expenses.API.Controllers.Authentication.Request
{
    public class GetLoginMethodsRequest
        : IRequest<List<CredentialType>>
    {
        public readonly User User;

        public GetLoginMethodsRequest(User user)
        {
            this.User = user;
        }
    }
}
