using Expenses.Data.Model.Authentication.Users;
using FluentValidation;

namespace Expenses.API.Validators.Model.Authentication
{
    public class UserHasUsernameValidator
        : AbstractValidator<User>
    {
        public UserHasUsernameValidator()
        {
            this.RuleFor(it => it.Username)
                .NotEmpty();
        }
    }
}
