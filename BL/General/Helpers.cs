using System.Transactions;

namespace Resunet.BL.General
{
    public static class Helpers
    {
        public static int? stringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

        /* Если мы начинаем дебажить, то транзакция на паузу не становится,
         * пока мы дебажим и она может устареть и возникнут проблемы
         * и для этого время нужно поставить побольше
         */
        public static TransactionScope CreateTransactionScope(int seconds = 6000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
