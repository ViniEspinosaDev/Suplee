using Flurl.Http;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.Mail;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.ExternalService.Imgbb.Services
{
    public class ImgbbService : IImgbbService
    {
        private readonly ImgbbConfiguration _imgbbConfiguracao;

        public ImgbbService(IEnvironment environment)
        {
            _imgbbConfiguracao = environment.ConfiguracaoImgbb;
        }

        public async Task<ResponseApi<ImgbbUploadImageResponse>> UploadImage(ImgbbUploadInputModel imgbbUploadInputModel)
        {
            var url = $"{_imgbbConfiguracao.Url}/1/upload?key={_imgbbConfiguracao.Key}";

            FlurlClient flurlClient = new FlurlClient(url);

            try
            {
                var response = await flurlClient.Request()
                    .PostUrlEncodedAsync(imgbbUploadInputModel)
                    .ReceiveJson<ImgbbUploadImageResponse>();

                return await Task.FromResult(new ResponseApi<ImgbbUploadImageResponse> { Success = true, Errors = null, Data = response });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new ResponseApi<ImgbbUploadImageResponse> { Success = false, Errors = new List<string> { ex.Message } });
            }
        }
    }
}
