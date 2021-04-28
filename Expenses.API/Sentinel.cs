using System.Reflection;

namespace Expenses.API
{
    public static class Sentinel
    {
        public static readonly Assembly Assembly = typeof(Sentinel).Assembly;
    }
}
