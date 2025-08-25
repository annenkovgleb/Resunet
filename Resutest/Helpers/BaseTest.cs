using Microsoft.AspNetCore.Http;
using ResunetBl.Auth;
using ResunetBl.Profile;
using ResunetBl.General;
using ResunetDAL.Interfaces;
using ResunetDAL.Implementations;
using Auth = ResunetDAL.Implementations.AuthDAL;
using DbSession = ResunetDAL.Implementations.DbSessionDAL;
using IAuthDAL = ResunetDAL.Interfaces.IAuthDAL;
using IDbSessionDAL = ResunetDAL.Interfaces.IDbSessionDAL;
using IProfileDAL = ResunetDAL.Interfaces.IProfileDAL;
using Profile = ResunetDAL.Implementations.ProfileDAL;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDAL Auth = new Auth();
        protected IEncrypt encrypt = new Resunet.Deps.Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected ResunetBl.Auth.IAuth authBL;
        protected IDbSessionDAL DbSession = new DbSession();
        protected ResunetBl.Auth.IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfileDAL Profile = new Profile();
        protected IUserTokenDAL UserToken = new UserTokenDAL();
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
