using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Validators.Model.Authentication;
using FluentValidation;

namespace Expenses.API.Validators.Request.Authentication.Credentials
{
    public class GetLoginMethodsRequestValidator
        : AbstractValidator<GetLoginMethodsRequest>
    {
        public GetLoginMethodsRequestValidator()
        {
            this.RuleFor(it => it.User)
                .NotNull()
                .SetValidator(new UserHasUsernameValidator());
        }
    }
}
