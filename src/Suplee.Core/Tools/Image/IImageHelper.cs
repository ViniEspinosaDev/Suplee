namespace Suplee.Core.Tools.Image
{
    public interface IImageHelper
    {
        byte[] GenerateThumb(byte[] img);
        byte[] CropImageHeight(byte[] img, int height);
        byte[] CropImageWidth(byte[] img, int width);
        byte[] ResizeImage(byte[] img, int width, int height);
    }
}
