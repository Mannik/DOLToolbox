using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;

namespace MannikToolbox.Services
{
    public class ModelImageService
    {
        private static string BasePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Assets\Models\";

        public Image LoadMob(int modelId)
        {
            return LoadAsset(modelId, "mobs");
        }

        public Image LoadItem(int modelId)
        {
            return LoadAsset(modelId, "items");
        }
        public Image LoadMob(int modelId, int maxWidth, int maxHeight)
        {
            var image = LoadMob(modelId);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        public Image LoadItem(int modelId, int maxWidth, int maxHeight)
        {
            var image = LoadItem(modelId);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        private Image LoadAsset(int modelId, string type)
        {
            var filePath = $@"{BasePath}{type}\{modelId}.jpg";

            if (!File.Exists(filePath))
            {
                return DownloadAsset(modelId, filePath, type);
            }

            return Image.FromFile(filePath);
        }

        private Image DownloadAsset(int modelId, string filePath, string type)
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
                    webClient.DownloadFile($"http://www.dolserver.net/models/Models/{type}/{modelId}.jpg", filePath);
                    return Image.FromFile(filePath);
                }
                catch (WebException)
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