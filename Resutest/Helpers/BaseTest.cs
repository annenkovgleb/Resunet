using Microsoft.AspNetCore.Http;
using ResunetBl.Auth;
using ResunetBl.Profile;
using ResunetBl.General;
using ResunetDal.Interfaces;
using ResunetDal.Implementations;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDAL authDAL = new AuthDAL();
        protected IEncrypt encrypt = new Resunet.Deps.Encrypt();
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
