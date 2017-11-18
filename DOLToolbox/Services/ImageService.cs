using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DOLToolbox.Services
{
    public class ImageService
    {
        private static string BasePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Assets\";

        private enum ModelType
        {
            Item, Mob
        }


        public async Task<Image> LoadMob(int modelId, int maxWidth, int maxHeight)
        {
            var image = await LoadAsset(modelId, ModelType.Mob);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        public async Task<Image> LoadItem(int modelId, int maxWidth, int maxHeight)
        {
            var image = await LoadAsset(modelId, ModelType.Item);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        public Image Load(string filename, int maxWidth, int maxHeight)
        {
            var image = LoadAsset(filename);
            return ScaleImage(image, maxWidth, maxHeight);
        }

        private async Task<Image> LoadAsset(int modelId, ModelType type)
        {
            return await Task.Run(() =>
            {
                if (type == ModelType.Item)
                {
                    var item = ModelViewerService.Viewer.GetItem(modelId);

                    return item == null ? null : ModelViewerService.Viewer.GetItemPicture(item);
                }

                var mob = ModelViewerService.Viewer.GetMob(modelId);

                return mob == null ? null : ModelViewerService.Viewer.GetMobPicture(mob);
            });
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