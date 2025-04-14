using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using ResunetBl.Auth;
using SixLabors.ImageSharp;

namespace ResunetBl.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    // этот тег требует, чтобы пользователь был авторизован для доступа к методу (к Profile)
    public class SiteAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public SiteAuthorizeAttribute() // нельзя юзать Dependency Injection
        {
        }

        // поэтому Dependency получаем другим способом
        // IAsyncAuthorizationFilter - для доскональной проверки
        // IAuthorizationFilter - для упрощенной и тогда метод будет типа void
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
            if (currentUser == null)
                throw new Exception("No user middleware");

            bool isLoggedIn = await currentUser.IsLoggedIn();
            if (isLoggedIn == false)
            {
                context.Result = new RedirectResult("/Login");
                return;
            }
        }
    }
}
