using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;

namespace Resunet.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly ICurrentUser currentUser;
        
        public MainMenuViewComponent(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await currentUser.IsLoggedIn();
            return View("Index", isLoggedIn);
        }
    }
}
