using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using Resunet.Service;
using Resunet.Middleware;

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
            //  if (ModelState.IsValid())
            // доступ к данным файла (картинки)
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                WebFile webfile = new WebFile();
                string filename = webfile.GetWebFilename(imageData.FileName);
                await webfile.UploadAndResizeImage(imageData.OpenReadStream(), filename, 800, 600);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}

