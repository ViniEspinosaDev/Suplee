namespace Suplee.Core.Tools.Image
{
    public interface IImageHelper
    {
        byte[] GenerateThumb(byte[] img, EImageType imageType);
        byte[] CropImageHeight(byte[] img, int height, EImageType imageType);
        byte[] CropImageWidth(byte[] img, int width, EImageType imageType);
        byte[] CropImage(byte[] img, int width, int height, EImageType imageType);
        byte[] ResizeImage(byte[] img, int width, int height, EImageType imageType);
    }
}
