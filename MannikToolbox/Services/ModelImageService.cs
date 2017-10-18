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
    }
}