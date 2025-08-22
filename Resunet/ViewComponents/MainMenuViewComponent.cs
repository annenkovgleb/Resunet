using Microsoft.AspNetCore.Mvc;
using ResunetBl.Auth;

namespace Resunet.ViewComponents;

public class MainMenuViewComponent(ICurrentUser currentUser) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        bool isLoggedIn = await currentUser.IsLoggedIn();
        return View("Index", isLoggedIn);
    }
}