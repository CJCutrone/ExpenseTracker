using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Expenses.Data.Access.Queries
{
    public interface IExpenseQuery<TParameter, TResponse>
    {
        public async Task<TResponse> Execute(DbConnection connection, TParameter parameter)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return await OnExecute(connection, parameter).ConfigureAwait(false);
        }

        public abstract Task<TResponse> OnExecute(DbConnection connection, TParameter parameter);
    }
}
