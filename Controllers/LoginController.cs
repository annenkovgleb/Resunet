using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.ViewModels;

namespace Resunet.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBl;

        public LoginController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(
                        model.Email!, model.Password!,
                        model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Resunet.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email",
                        "Имя или Email неверные");
                }
            }
            return View("Index", model);
        }
    }
}

