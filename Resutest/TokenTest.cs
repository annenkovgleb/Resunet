using System.Transactions;
using Resutest.Helpers;

namespace Resutest
{
    public class TokenTest : Helpers.BaseTest
    {
        [Test]
        public async Task BaseTokenTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var tokenId = await this.userTokenDAL.Create(10);
                var userid = await this.userTokenDAL.Get(tokenId);
                Assert.That(userid, Is.EqualTo(10));
            }
        }
    }
}
