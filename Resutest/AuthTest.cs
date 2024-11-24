using Resutest.Helpers;
using System.Transactions;
using Resunet.BL;

namespace Resutest
{
    public class AuthTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new Resunet.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });
                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult(); });
                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });
                await authBL.Authenticate(email, "qwer1234", false);
            }
        }
    }
}
