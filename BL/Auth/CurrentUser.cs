using Resunet.BL.Auth;

namespace Resunet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor; // доступ к сессии 
        private readonly IDbSession dbSession;

        public CurrentUser(
            IHttpContextAccessor httpContextAccessor,
            IDbSession dbSession)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
        }

        public async Task<bool> IsLoggedIn()
        {
            return await dbSession.IsLoggedIn();
        }
    }
}