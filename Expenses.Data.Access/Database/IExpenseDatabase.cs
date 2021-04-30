using Expenses.Data.Access.Queries;
using System.Data.Common;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Database
{
    public interface IExpenseDatabase
    {
        public DbConnection Connection { get; set; }

        public Task<TResponse> PrepareAndExecute<TParameters, TResponse>(
            IExpenseQuery<TParameters, TResponse> query,
            TParameters parameters
        ) => query.Execute(Connection, parameters);
    }
}
