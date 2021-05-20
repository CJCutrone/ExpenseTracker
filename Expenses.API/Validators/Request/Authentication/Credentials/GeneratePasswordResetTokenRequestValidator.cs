using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.API.Validators.Model.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.API.Validators.Request.Authentication.Credentials
{
    public class GeneratePasswordResetTokenRequestValidator
        : AbstractValidator<GeneratePasswordResetTokenRequest>
    {
        public GeneratePasswordResetTokenRequestValidator()
        {
            this.RuleFor(it => it.User)
                .NotNull()
                .SetValidator(new UserHasEmailValidator());
        }
    }
}
