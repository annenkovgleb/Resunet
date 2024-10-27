using System;
using Resunet.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using Resunet.ViewMapper;

// ничего не знает о DAL уровне, его прерогатива работать только с BL уровнем

namespace Resunet.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL authBl;

        public RegisterController(IAuthBL authBl)
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
            if (ModelState.IsValid)
            {
                // если какой-то косяк с Email, то скипаем
                var errorModel = await authBl.ValidateEmail(model.Email ?? "");
                if (errorModel != null)
                {
                    ModelState.TryAddModelError("Email", errorModel.ErrorMessage!);
                }
            }

            // и если модель все еще валидная => создаем пользователя
            if (ModelState.IsValid)
            {
                await authBl.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }

            // файл "Index" будет искаться, по умолчанию, в папке с названием класса но без 
            // "Controller", т.е. "Register" и если в папке "View" указать папку "Register" как-то
            // по-другому, то придется здесь придется указывать полное имя (где его искать)
            return View("Index", model);
        }
    }
}

