using Resutest.Helpers;
using System.Transactions;
using ResunetBl.Auth;
using ResunetBl.Exeption;

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
                    new ResunetDal.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });
                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult(); });
                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });
                await authBL.Authenticate(email, "qwer1234", false);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
        }

        public async Task RemoveMeTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                int userId = await authBL.CreateUser(
                    new ResunetDal.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                await authBL.Authenticate(email,"qwer1234",true);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
        }
    }
}
