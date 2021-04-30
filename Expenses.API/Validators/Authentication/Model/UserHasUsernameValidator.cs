using Expenses.Data.Model.Authentication.Users;
using FluentValidation;

namespace Expenses.API.Validators.Authentication.Model
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
