using Microsoft.AspNetCore.Mvc;
using ResunetBl.Middleware;
using ResunetBl.ViewModels;
using ResunetBl.Auth;
using ResunetBl.Exeption;

namespace ResunetBl.Controllers
{
    [SiteNotAuthorize()]
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
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }
            return View("Index", model);
        }
    }
}

