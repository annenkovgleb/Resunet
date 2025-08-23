using Microsoft.AspNetCore.Mvc;
using Resunet.Middleware;
using Resunet.ViewModels;
using ResunetBl.Auth;
using ResunetBl.Exeption;

namespace Resunet.Controllers;

[SiteNotAuthorize]
public class LoginController(IAuth _authBl) : Controller
{
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
        => View("Index", new LoginViewModel());

    [HttpPost]
    [Route("/login")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
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