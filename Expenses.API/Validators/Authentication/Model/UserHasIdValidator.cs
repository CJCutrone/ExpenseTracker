using Expenses.Data.Model.Authentication.Users;
using FluentValidation;

namespace Expenses.API.Validators.Authentication.Model
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
