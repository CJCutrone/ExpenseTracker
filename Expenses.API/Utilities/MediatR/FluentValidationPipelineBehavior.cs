using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.API.Utilities.MediatR
{
    public class FluentValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public FluentValidationPipelineBehavior(
            IEnumerable<IValidator<TRequest>> validators
        )
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResults =
                validators
                    .Select(validator => validator.Validate(request))
                    .SelectMany(results => results.Errors)
                    .ToList();

            if (validationResults.Any())
                throw new ValidationException(validationResults);

            return next();
        }
    }
}
