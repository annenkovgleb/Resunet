using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;

namespace Resunet.BL.General
{
    public class WebCookie : IWebCookie
    {
        private IHttpContextAccessor httpContextAccessor;
        public WebCookie(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Add(string cookieName, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";

        }

        public void AddSecure(string cookieName, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                AuthConstants.SessionCookieName, value.ToString(), options);
        }

        public void Delete(string cookieName)
        {
            httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionCookieName);
        }

        public string? Get(string cookieName)
        {
            var cookie = httpContextAccessor ?;
            if(cookie!=null&&cookie.Value.Value != null)  
                return cookie.Value.Value;
            return null;
        }
    }
}
