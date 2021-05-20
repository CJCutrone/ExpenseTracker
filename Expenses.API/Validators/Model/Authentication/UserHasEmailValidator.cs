using Expenses.Data.Model.Authentication.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.API.Validators.Model.Authentication
{
    public class UserHasEmailValidator
        : AbstractValidator<User>
    {
        public UserHasEmailValidator()
        {
            this.RuleFor(it => it.Email)
                .NotEmpty();
        }
    }
}
