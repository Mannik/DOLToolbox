using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace DOLToolbox.Services
{
    public class ImageService
    {
        private static string BasePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Assets\";

        public async Task<Image> LoadMob(int modelId)
        {
            return await LoadAsset(modelId, @"Models\mobs");
        }

        public async Task<Image> LoadItem(int modelId)
        {
            return await LoadAsset(modelId, @"Models\items");
        }

        public async Task<Image> LoadMob(int modelId, int maxWidth, int maxHeight)
        {
            var image = await LoadMob(modelId);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        public async Task<Image> LoadItem(int modelId, int maxWidth, int maxHeight)
        {
            var image = await LoadItem(modelId);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        public Image Load(string filename)
        {
            return LoadAsset(filename);
        }

        public Image Load(string filename, int maxWidth, int maxHeight)
        {
            var image = LoadAsset(filename);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        private async Task<Image> LoadAsset(int modelId, string path)
        {
            var filePath = $@"{BasePath}{path}\{modelId}.jpg";

            if (!File.Exists(filePath))
            {
                return await DownloadAsset(modelId, filePath, path);
            }

            return Image.FromFile(filePath);
        }

        private Image LoadAsset(string file)
        {
            var filePath = $@"{BasePath}\{file}";

            if (!File.Exists(filePath))
            {
                return null;
            }

            return Image.FromFile(filePath);
        }

        private async Task<Image> DownloadAsset(int modelId, string filePath, string type)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var webClient = new WebClient())
            {
                try
                {
                    var url = $"www.dolserver.net/models/{(type.Replace("\\", "/"))}/{modelId}.jpg";
                    var data = await webClient.DownloadDataTaskAsync(new Uri("http://" + url));
                    var image = Image.FromStream(new MemoryStream(data));

                    image.Save(filePath);
                    return image;
                }
                catch (WebException ex)  when(ex.Message == "The remote server returned an error: (404) Not Found.")
                {
                }
            }

            return null;
        }

        private static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            if (image == null)
            {
                return null;
            }

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            image.Dispose();

            return newImage;
        }
    }
}