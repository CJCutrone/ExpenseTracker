using Expenses.API.Controllers.Authentication.Credentials.Request;
using Expenses.Data.Access.Database;
using Expenses.Data.Access.Queries.Authentication.Credentials;
using Expenses.Data.Model.Authentication.Credentials;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Handler.Authentication.Credentials
{
    public class GetLoginMethodsRequestHandler
        : IRequestHandler<GetLoginMethodsRequest, List<CredentialType>>
    {
        public IExpenseDatabase Database;
        public GetLoginMethodsRequestHandler(
            IExpenseDatabase database
        )
        {
            this.Database = database;
        }

        public async Task<List<CredentialType>> Handle(GetLoginMethodsRequest request, CancellationToken cancellationToken)
            => await Database.PrepareAndExecute(new GetLoginMethodQuery(), request.User);
    }
}
