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
                ((TestCookie)webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                // получить сессию из бд и убедиться, что она там существует
                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession!.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                await this.dbSession.SetUserId(10);

                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");
                Assert.That(dbSession!.UserId, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task AddValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                await this.dbSession.SetUserId(10);
                await this.dbSession.UpdateSessionData();

                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");
                Assert.That(dbSession!.UserId, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                dbSession.ResetSessionCache();
                dbSession.AddValue("Test", "TestValue");
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));

                dbSession.AddValue("Test", "UpdateValue");
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdateValue"));

                await dbSession.UpdateSessionData();

                dbSession.ResetSessionCache();
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdateValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                dbSession.ResetSessionCache();
                dbSession.AddValue("Test", "TestValue");
                await dbSession.UpdateSessionData();

                dbSession.RemoveValue("Test");
                await dbSession.UpdateSessionData();

                dbSession.ResetSessionCache();
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo(""));
            }
        }
    }
}