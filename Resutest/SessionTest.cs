using System.Transactions;
using Resutest.Helpers;

namespace Resutest
{
    public class SessionTest : BaseTest
    {
        [Test]
        [NonParallelizable] // выполняются последовательно, сначала один, потом второй
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                // получить сессию из бд и убедиться, что она там существует
                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable] // выполняются последовательно, сначала один, потом второй
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                // чистка кук
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                // получить сессию из бд и убедиться, что она там существует
                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");
                Assert.That(dbSession.UserId!, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }
    }
}
