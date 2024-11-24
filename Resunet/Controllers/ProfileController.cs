using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using Resunet.Service;
using Resunet.Middleware;
using Resunet.BL.Auth;
using Resunet.BL.Profile;
using Resunet.ViewMapper;
using Resunet.DAL.Models;

namespace Resunet.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            this.currentUser = currentUser;
            this.profile = profile;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            int? userid = await currentUser.GetCurrentUserId();
            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);

            ProfileModel profileModel = profiles.FirstOrDefault()!;
            ProfileMapper.MapProfileModelToProfileViewModel();

            return View();
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

