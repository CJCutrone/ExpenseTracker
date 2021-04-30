using Expenses.API.Controllers.Authentication.Request;
using Expenses.Data.Model.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expenses.API.Controllers.Authentication
{
    [ApiController]
    public class GetLoginMethodsController
        : ControllerBase
    {
        private readonly IMediator mediator;

        public GetLoginMethodsController(
            IMediator mediator
        )
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("api/user/{username:required}/logins")]
        public async Task<List<CredentialType>> Get(string username)
            => await this.mediator.Send(new GetLoginMethodsRequest(new User(username)));
    }
}
