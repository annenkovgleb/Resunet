using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Resunet.Service
{
    public class WebFile
    {
        const string FOLDER_PREFIX = "./wwwroot";

        public WebFile() { }


        // изменить имя файла, если превышает кол-во символов 


        public string GetWebFilename(string filename)
        {
            string directory = GetWebFileFolder(filename);

            CreateFolder(FOLDER_PREFIX + directory);

            // в середину можно добавить какую-то уникальность для названия файла
            return directory + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
        }

        public string GetWebFileFolder(string filename)
        {
            MD5 md5hash = MD5.Create(); // MD5 - бывают перебои с коллизиями
            byte[] inputBytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            // 1 сабстринг - 1ая папка из 2х символов, 2 - следующая папка
            return "/images/" + hash.Substring(0, 2) + "/" +
                    hash.Substring(0, 4);
        }

        public void CreateFolder(string directory)
        {
            // если папка не существует, то .NET грохнется (раньше так было)
            // создавать файлы в несуществующей папке нельзя, нужно ее сначала создать 
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth, int newHeight)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

                // int height = image.Height / 2;
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

                await image.SaveAsJpegAsync(FOLDER_PREFIX + filename, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}
