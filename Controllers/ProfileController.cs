using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using static System.Net.WebRequestMethods;
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
        public async Task<IActionResult> IndexSave()
        {
            string filename = "";
            // доступ к данным файла (картинки профиля) 
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                MD5 md5hash = MD5.Create(); // MD5 - бывают перебои с коллизиями
                // байты для хеширования 
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName);
                byte[] hashBytes = md5hash.ComputeHash(inputBytes);

                string hash = Convert.ToHexString(hashBytes);

                // 1 сабстринг - 1ая папка из 2х символов, 2 - следующая папка
                var directory = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);

                // если папка не существует, то .NET грохнется (раньше так было)
                // создавать файлы в несуществующей папке нельзя, нужно ее сначала создать 
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // в середину можно добавить какую-то уникальность для названия файла
                filename = directory + "/" + imageData.FileName;
                using (var stream = System.IO.File.Create(filename))
                    await imageData.CopyToAsync(stream);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}