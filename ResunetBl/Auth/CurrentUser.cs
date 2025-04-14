using ResunetBl.General;
using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Auth
{
    public class CurrentUser(
        IDbSession _dbSession,
        IWebCookie _webCookie,
        IUserToken userToken,
        IProfile profile)
        : ICurrentUser
    {
        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = _webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie is null)
                return null;

            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");

            if (tokenGuid is null)
                return null;

            int? userid = await userToken.Get((Guid)tokenGuid);
            return userid;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await _dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserIdByToken();
                if (userid is not null)
                {
                    await _dbSession.SetUserId((int)userid);
                    isLoggedIn = true;
                }
            }

            return isLoggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await _dbSession.GetUserId();
        }

        public async Task<IEnumerable<ProfileModel>> GetProfiles()
        {
            int? userid = await GetCurrentUserId();
            if (userid is null)
            {
                throw new Exception("Пользователь не найден");
            }

            return await profile.GetByUserId((int)userid);
        }
    }
}