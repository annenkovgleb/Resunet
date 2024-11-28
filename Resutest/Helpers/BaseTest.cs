using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;
using Resunet.BL.General;
using Resunet.BL.Profile;
using Resunet.DAL;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDAL authDAL = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IProfile profile;

        protected CurrentUser currentUser;

        public BaseTest()
        {
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDAL, encrypt, webCookie, dbSession, userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
            profile = new Profile(profileDAL);
        }
    }
}
