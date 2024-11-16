using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using Resunet.Middleware;
using Resunet.Service;

namespace Resunet.Controllers
{
    [SiteAuthorize()]
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave()
        {
            // if(ModelState.IsValid())
            // доступ к данным файла (картинки профиля) 
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                WebFile webFile = new WebFile();
                string filename = webFile.GetWebFileFolder(imageData.FileName);
                await webFile.UploadAndResizeImage(imageData.OpenReadStream(), filename, 800, 600);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}