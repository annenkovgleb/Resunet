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
            var profiles = await currentUser.GetProfiles();

            ProfileModel? profileModel = profiles.FirstOrDefault();

            return View(profileModel != null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile/uploadimage")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ImageSave(int? profileId)
        {
            int? userid = await currentUser.GetCurrentUserId();
            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);
            if (profileId != null && !profiles.Any(m => m.ProfileId == profileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = profiles.FirstOrDefault(m => m.ProfileId == profileId) ?? new ProfileModel();
                profileModel.UserId = (int)userid;

                // Request.Form.Files[0] != null при обращении к нулевому массиву, если файл никто не выберет, то здесь все рухнет
                // Request.Form.Files.Count > 0 - защищаем, чтобы не рухалось
                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    WebFile webfile = new WebFile();
                    string filename = webfile.GetWebFilename(Request.Form.Files[0].FileName);
                    await webfile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
                    profileModel.ProfileImage = filename;
                }
                await profile.AddOrUpdate(profileModel);
            }
            return Redirect("/profile");
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userid = await currentUser.GetCurrentUserId();

            // на всякий случай, можно удалить, тк есть [SiteAuthorize()]
            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);
            if (model.ProfileId != null && !profiles.Any(m => m.ProfileId == model.ProfileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model); // нужна на бэке
                profileModel.UserId = (int)userid;
                await profile.AddOrUpdate(profileModel);
                return Redirect("/");
            }
            return View("Index", new ProfileViewModel());
        }
    }
}

