using Flurl.Http;
using Microsoft.Extensions.Options;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.ExternalService.Imgbb.Services
{
    public class ImgbbService : IImgbbService
    {
        private readonly ImgbbConfiguracao _imgbbConfig;
        private string key = "5f6e7b6c08b780e10bdbbd3ed9ae7d6b";

        public ImgbbService(IOptions<ImgbbConfiguracao> imgbbConfiguracao)
        {
            _imgbbConfig = imgbbConfiguracao.Value;
        }

        public async Task<ResponseApi<ImgbbUploadImageResponse>> UploadImage(ImgbbUploadInputModel imgbbUploadInputModel)
        {
            var url = $"{_imgbbConfig.Url}/1/upload?key={key}";

            FlurlClient flurlClient = new FlurlClient(url);

            try
            {
                var response = await flurlClient.Request()
                    .PostUrlEncodedAsync(imgbbUploadInputModel) // TODO: Erro aqui provavelmente, pesquisar como enviar
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
