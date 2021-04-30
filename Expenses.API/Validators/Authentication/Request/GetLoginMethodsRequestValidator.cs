using Expenses.API.Controllers.Authentication.Request;
using Expenses.API.Validators.Authentication.Model;
using FluentValidation;

namespace Expenses.API.Validators.Authentication.Request
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
