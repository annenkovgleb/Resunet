using Microsoft.AspNetCore.Mvc;
using ResunetBl.ViewModels;
using ResunetBl.ViewMapper;
using ResunetBl.Middleware;
using ResunetBl.Auth;
using ResunetBl.Exeption;

// ничего не знает о DAL уровне, его прерогатива работать только с BL уровнем

namespace ResunetBl.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth authBl;

        public RegisterController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()  // отображение формы для регистрации 
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {

            // и если модель все еще валидная => создаем пользователя
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
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

