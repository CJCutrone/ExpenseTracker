using Expenses.Data.Model.Authentication.Users;
using FluentValidation;

namespace Expenses.API.Validators.Model.Authentication
{
    public class UserHasIdValidator
        : AbstractValidator<User>
    {
        public UserHasIdValidator()
        {
            this.RuleFor(it => it.Id)
                .NotNull();
        }
    }
}
