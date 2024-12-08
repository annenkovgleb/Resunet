using System.Transactions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                // чистка кук
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                await this.dbSession.SetUserId(10);

                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");
                Assert.That(dbSession.UserId!, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await this.currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task AddValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                await this.dbSession.UpdateSessionData();

                var dbSession = await dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");
                Assert.That(dbSession.UserId!, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await this.currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));

                this.dbSession.AddValue("Test", "UpdateValue");
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdateValue"));

                await this.dbSession.UpdateSessionData();

                this.dbSession.ResetSessionCache();
                session = await this.dbSession.GetSession();
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdateValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                this.dbSession.AddValue("Test", "TestValue");
                await this.dbSession.UpdateSessionData();

                this.dbSession.RemoveValue("Test");
                await this.dbSession.UpdateSessionData();

                this.dbSession.ResetSessionCache();
                session = await this.dbSession.GetSession();
                Assert.That(this.dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo(""));
            }
        }
    }
}
