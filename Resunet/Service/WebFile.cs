using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Resunet.Service;




public class WebFile
{
    const string FOLDER_PREFIX = "./wwwroot";

    public WebFile()
    {
    }

    public string GetWebFileName(string fileName, string folder = "images")
    {
        string directory = GetWebFileFolder(fileName, folder);

        CreateFolder(FOLDER_PREFIX + directory);

        // в середину можно добавить какую-то уникальность для названия файла
        return directory + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
    }

    public string GetWebFileFolder(string fileName, string folder = "images")
    {
        MD5 md5hash = MD5.Create(); // MD5 - бывают перебои с коллизиями
        byte[] inputBytes = Encoding.ASCII.GetBytes(fileName);
        byte[] hashBytes = md5hash.ComputeHash(inputBytes);

        string hash = Convert.ToHexString(hashBytes);

        return "/" + folder + "/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
    }

    public void CreateFolder(string directory)
    {
        // создавать файлы в несуществующей папке нельзя, нужно ее сначала создать 
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    public async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
    {
        using (Image image = await Image.LoadAsync(fileStream))
        {
            int aspectWidth = newWidth;
            int aspectHeight = newHeight;

            if (image.Width / (image.Height / (float)newHeight) > newWidth)
                aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
            else
                aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

            image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

            await image.SaveAsJpegAsync(FOLDER_PREFIX + fileName, new JpegEncoder() { Quality = 75 });
        }
    }
}