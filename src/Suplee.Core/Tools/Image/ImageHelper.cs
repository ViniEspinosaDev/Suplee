using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Suplee.Core.Tools.Image
{
    public class ImageHelper : IImageHelper
    {
        public byte[] GenerateThumb(byte[] img)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Max
            };
            return Resize(image, options);
        }

        public byte[] CropImageHeight(byte[] img, int height)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(image.Width, height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options);
        }

        public byte[] CropImageWidth(byte[] img, int width)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, image.Height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options);
        }

        public byte[] CropImage(byte[] img, int width, int height)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options);
        }

        public byte[] ResizeImage(byte[] img, int width, int height)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max
            };
            return Resize(image, options);
        }

        private byte[] Resize(Image<Rgba32> image, ResizeOptions resizeOptions)
        {
            byte[] ret;

            image.Mutate(i => i.Resize(resizeOptions));

            using MemoryStream ms = new MemoryStream();
            image.SaveAsJpeg(ms);

            ret = ms.ToArray();
            ms.Close();

            return ret;
        }
    }
}
