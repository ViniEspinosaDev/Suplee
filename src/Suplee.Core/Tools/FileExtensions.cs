using Microsoft.AspNetCore.Http;

namespace Suplee.Core.Tools
{
    public static class FileExtensions
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            using var fileStream = formFile.OpenReadStream();
            var bytes = new byte[formFile.Length];
            fileStream.Read(bytes, 0, (int)formFile.Length);

            return bytes;
        }
    }
}
