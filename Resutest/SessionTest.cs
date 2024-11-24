using System.Transactions;
using Resutest.Helpers;

namespace Resutest
{
    public class SessionTest : BaseTest
    {
        [Test]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                // получить сессию
                var session = await this.dbSession.GetSession();

                // получить сессию из бд и убедиться, что она там существует
                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession);

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }
    }
}
