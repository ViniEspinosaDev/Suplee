using Suplee.ExternalService.Imgbb.DTO;
using System.Threading.Tasks;

namespace Suplee.ExternalService.Imgbb.Interfaces
{
    public interface IImgbbService
    {
        Task<ResponseApi<ImgbbUploadImageResponse>> UploadImage(ImgbbUploadInputModel imgbbUploadInputModel);
    }
}
