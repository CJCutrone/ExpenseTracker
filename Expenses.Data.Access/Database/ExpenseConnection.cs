using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Data.SqlClient;

namespace Expenses.Data.Access.Database
{
    public class ExpenseConnection
        : IExpenseDatabase
    {
        public DbConnection Connection { get; set; }

        public ExpenseConnection(
            IOptionsSnapshot<ExpenseConnectionSettings> settings
        )
        {
            this.Connection = new SqlConnection(settings.Value.ConnectionString);
        }
    }
}
