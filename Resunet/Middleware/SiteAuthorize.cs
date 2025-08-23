using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResunetBl.Auth;

namespace Resunet.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
// этот тег требует, чтобы пользователь был авторизован для доступа к методу (к Profile)
public class SiteAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public SiteAuthorizeAttribute()
    {
    }

    // поэтому Dependency получаем другим способом
    // IAsyncAuthorizationFilter - для доскональной проверки
    // IAuthorizationFilter - для упрощенной и тогда метод будет типа void
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser is null)
        {
            throw new Exception("No user middleware");
        }

        bool isLoggedIn = await currentUser.IsLoggedIn();
        if (!isLoggedIn)
        {
            context.Result = new RedirectResult("/Login");
        }
    }
}