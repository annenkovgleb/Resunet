using System;
namespace Resunet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor; // для доступа к сессии 
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

