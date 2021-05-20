using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Controllers.Authentication.Credentials.Response;
using Expenses.Data.Model.Authentication.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Expenses.API.Controllers.Authentication.Credentials
{
    [ApiController]
    public class GeneratePasswordResetTokenController
        : ControllerBase
    {
        private readonly IMediator mediator;

        public GeneratePasswordResetTokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("api/user/{email:required}/reset")]
        public async Task<GeneratePasswordResetResponse> Get(string email)
            => await this.mediator.Send(new GeneratePasswordResetTokenRequest(new User() { Email = email }));
    }
}
