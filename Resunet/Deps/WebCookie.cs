using ResunetBl.General;

namespace Resunet.Deps;

// обертка, чтобы не реализовывать все с HttpContext
public class WebCookie(IHttpContextAccessor httpContextAccessor) : IWebCookie
{
    public void Add(string cookieName, string value, int days = 0)
    {
        CookieOptions options = new CookieOptions();
        options.Path = "/";
        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);

        httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void AddSecure(string cookieName, string value, int days = 0)
    {
        CookieOptions options = new CookieOptions();
        options.Path = "/";
        options.HttpOnly = true;
        options.Secure = true;

        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);

        httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Delete(string cookieName)
    {
        httpContextAccessor?.HttpContext?.Response.Cookies.Delete(cookieName);
    }

    public string? Get(string cookieName)
    {
        var cookie =
            httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m => m.Key == cookieName);

        if (cookie is not null && cookie.Value.Value is not null)
        {
            return cookie.Value.Value;
        }

        return null;
    }
}