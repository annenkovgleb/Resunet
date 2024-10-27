using System;
namespace Resunet.BL.Auth
{
	public class CurrentUser: ICurrentUser
	{
        // httpContextAccessor - для доступа к сессии 
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
        }

		public bool IsLoggedIn()
		{
            // проверяем на userid, если user сохраняется в сессии => он авторизован  
            int? id = httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME);
			return id != null;
        }
    }
}

