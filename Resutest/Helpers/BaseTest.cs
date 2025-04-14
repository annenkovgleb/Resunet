using Microsoft.AspNetCore.Http;
using ResunetBl.Auth;
using ResunetBl.Profile;
using ResunetBl.General;
using ResunetDAL.Interfaces;
using ResunetDAL.Implementations;
using Auth = ResunetDAL.Implementations.Auth;
using DbSession = ResunetDAL.Implementations.DbSession;
using IAuth = ResunetDAL.Interfaces.IAuth;
using IDbSession = ResunetDAL.Interfaces.IDbSession;
using IProfile = ResunetDAL.Interfaces.IProfile;
using Profile = ResunetDAL.Implementations.Profile;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuth Auth = new Auth();
        protected IEncrypt encrypt = new Resunet.Deps.Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected ResunetBl.Auth.IAuth authBL;
        protected IDbSession DbSession = new DbSession();
        protected ResunetBl.Auth.IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfile Profile = new Profile();
        protected IUserToken UserToken = new UserToken();
        protected ResunetBl.Profile.IProfile profile;
        protected CurrentUser currentUser;

        public BaseTest()
        {
            webCookie = new TestCookie();
            dbSession = new ResunetBl.Auth.DbSession(DbSession, webCookie);
            authBL = new ResunetBl.Auth.Auth(Auth, encrypt, webCookie, dbSession, UserToken);
            currentUser = new CurrentUser(dbSession, webCookie, UserToken, Profile);
            // profile = new Profile(profileDAL);
        }
    }
}
