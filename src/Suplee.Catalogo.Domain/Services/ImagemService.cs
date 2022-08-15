using Microsoft.AspNetCore.Http;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Tools;
using Suplee.Core.Tools.Image;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Services
{
    public class ImagemService : IImagemService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IImgbbService _imgbbService;
        private readonly IImageHelper _imageHelper;

        public ImagemService(IMediatorHandler mediatorHandler, IImgbbService imgbbService, IImageHelper imageHelper)
        {
            _mediatorHandler = mediatorHandler;
            _imgbbService = imgbbService;
            _imageHelper = imageHelper;
        }

        public async Task<ProdutoImagem> UploadImagem(IFormFile imagem)
        {
            if (!ValidarSeImagem(imagem)) return default(ProdutoImagem);

            try
            {
                var nomeImagem = imagem.FileName;
                var extensao = Path.GetExtension(nomeImagem);
                var nomeImagemSemExtensao = nomeImagem.Replace(extensao, string.Empty);

                byte[] bytesImagem = imagem.GetBytes();

                byte[] imagemReduzida = _imageHelper.CropImage(bytesImagem, 155, 235, RecuperarTipoImagem(extensao));
                byte[] imagemMaior = _imageHelper.CropImage(bytesImagem, 279, 423, RecuperarTipoImagem(extensao));

                var retornoOriginal = await _imgbbService
                    .UploadImage(new ImgbbUploadInputModel(ConverterBytesEmBase64(bytesImagem), nomeImagemSemExtensao));

                var retornoReduzida = await _imgbbService
                    .UploadImage(new ImgbbUploadInputModel(ConverterBytesEmBase64(imagemReduzida), nomeImagemSemExtensao));

                var retornoMaior = await _imgbbService
                    .UploadImage(new ImgbbUploadInputModel(ConverterBytesEmBase64(imagemMaior), nomeImagemSemExtensao));

                var produtoImagem = new ProdutoImagem(
                    produtoId: Guid.Empty,
                    nomeImagem: nomeImagemSemExtensao,
                    urlImagemOriginal: retornoOriginal.Data.data.Url,
                    urlImagemReduzida: retornoReduzida.Data.data.Url,
                    urlImagemMaior: retornoMaior.Data.data.Url);

                return produtoImagem;
            }
            catch (Exception excecao)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", $"Falha no upload de imagem. Retorno: {excecao.Message}"));
            }

            return default(ProdutoImagem);
        }

        private EImageType RecuperarTipoImagem(string extensao)
        {
            extensao = extensao.Replace(".", string.Empty);

            switch (extensao)
            {
                case "jpeg":
                case "jpg":
                    return EImageType.JPEG;
                case "png":
                    return EImageType.PNG;
                case "webp":
                    return EImageType.WEBP;
                default:
                    return EImageType.JPG;
            }
        }

        private bool ValidarSeImagem(IFormFile imagem)
        {
            var extensoesImagem = new List<string>() { "jpeg", "jpg", "png", "svg", "webp" };

            var ehImagem = false;

            foreach (var extensao in extensoesImagem)
            {
                if (!imagem.ContentType.Contains(extensao)) continue;

                ehImagem = true;
                break;
            }

            if (!ehImagem)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification("", "O arquivo não é uma imagem"));
                return false;
            }

            return true;
        }

        private string ConverterBytesEmBase64(byte[] bytes) =>
            Convert.ToBase64String(bytes, 0, bytes.Length);
    }
}
