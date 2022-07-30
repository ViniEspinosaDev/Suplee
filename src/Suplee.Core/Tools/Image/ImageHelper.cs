using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Suplee.Core.Tools.Image
{
    public class ImageHelper : IImageHelper
    {
        public byte[] GenerateThumb(byte[] img, EImageType imageType)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Max
            };
            return Resize(image, options, imageType);
        }

        public byte[] CropImageHeight(byte[] img, int height, EImageType imageType)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(image.Width, height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options, imageType);
        }

        public byte[] CropImageWidth(byte[] img, int width, EImageType imageType)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, image.Height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options, imageType);
        }

        public byte[] CropImage(byte[] img, int width, int height, EImageType imageType)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Crop
            };
            return Resize(image, options, imageType);
        }

        public byte[] ResizeImage(byte[] img, int width, int height, EImageType imageType)
        {
            using var image = SixLabors.ImageSharp.Image.Load(img);
            var options = new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max
            };
            return Resize(image, options, imageType);
        }

        private byte[] Resize(SixLabors.ImageSharp.Image image, ResizeOptions resizeOptions, EImageType imageType)
        {
            byte[] ret;

            image.Mutate(i => i.Resize(resizeOptions));

            using MemoryStream ms = new MemoryStream();

            switch (imageType)
            {
                case EImageType.JPEG:
                case EImageType.JPG:
                    image.SaveAsJpeg(ms);
                    break;
                case EImageType.PNG:
                    image.SaveAsPng(ms);
                    break;
                case EImageType.WEBP:
                    image.SaveAsWebp(ms);
                    break;
                default:
                    image.SaveAsJpeg(ms);
                    break;
            }

            ret = ms.ToArray();
            ms.Close();

            return ret;
        }
    }

    public enum EImageType
    {
        JPG = 0,
        JPEG = 1,
        PNG = 2,
        WEBP = 3
    }
}
