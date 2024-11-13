using Resunet.BL.Auth;
using Resunet.BL.General;
using Resunet.DAL;

namespace Resunet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL
            )
        {
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;

            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (tokenGuid == null) // сделал проверку и выкинул с сайта
                return null;

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
            return userid;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserIdByToken(); // полезли в токен
                if (userid != null) // если токен найден в пользователе
                {
                    await dbSession.SetUserId((int)userid); // обновить сессию
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }
    }
}