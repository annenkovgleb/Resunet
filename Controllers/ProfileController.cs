using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;

namespace Resunet.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        public IActionResult IndexSave()
        {
            return View();
        }
    }
}