using Microsoft.AspNetCore.Mvc;
using Resunet.Middleware;
using Resunet.ViewMapper;
using Resunet.ViewModels;
using ResunetBl.Auth;
using ResunetBl.Exeption;

namespace Resunet.Controllers
{
    [SiteNotAuthorize]
    public class RegisterController(IAuth _authBl) : Controller
    {
        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DublicateEmailExeption)
                {
                    ModelState.TryAddModelError("Email", "Email не существует");
                }
            }

            // файл "Index" будет искаться, по умолчанию, в папке с названием класса, но без 
            // "Controller", т.е. "Register" и если в папке "View" указать папку "Register" как-то
            // по-другому, то здесь придется указывать полное имя (где его искать)
            return View("Index", model);
        }
    }
}