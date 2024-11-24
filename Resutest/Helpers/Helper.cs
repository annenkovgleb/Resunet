using System.Transactions;

namespace Resutest.Helpers
{
    public static class Helper
    {
        // если тест не выполняется за секуду -> нахер его
        // медленные тесты = бесполезные тесты
        public static TransactionScope CreateTransactionScope(int seconds = 99999999)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
