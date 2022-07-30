using MediatR;
using Microsoft.AspNetCore.Http;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Tools;
using Suplee.Core.Tools.Image;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using Suplee.Teste.Domain.Interfaces;
using Suplee.Teste.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Teste.Domain.Commands
{
    public class TesteCommandHandler : IRequestHandler<CadastrarImagemCommand, bool>
    {
        private readonly ITesteRepository _testeRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IImgbbService _imgbbService;
        private readonly IImageHelper _imageHelper;

        public TesteCommandHandler(
            ITesteRepository testeRepository,
            IMediatorHandler mediatorHandler,
            IImgbbService imgbbService,
            IImageHelper imageHelper)
        {
            _testeRepository = testeRepository;
            _mediatorHandler = mediatorHandler;
            _imgbbService = imgbbService;
            _imageHelper = imageHelper;
        }

        public async Task<bool> Handle(CadastrarImagemCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            if (!ValidarSeImagem(request.Imagem)) return false;

            var nomeImagem = request.Imagem.FileName;
            var extensao = Path.GetExtension(nomeImagem);

            if (extensao.Contains("webp"))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Poxa amigão, infelizmente não estamos aceitando .webp ainda, sinto muito :("));
                return false;
            }

            var bytesImagem = request.Imagem.GetBytes();

            var nomeSemExtensao = nomeImagem.Replace(extensao, string.Empty);
            var extensaoCompleta = request.Imagem.ContentType;

            var imagemReduzida = _imageHelper.ResizeImage(bytesImagem, 155, 235);
            var imagemMaximizada = _imageHelper.ResizeImage(bytesImagem, 279, 423);

            var nomeReduzida = $"{nomeSemExtensao}_reduzida";
            var nomeMaximizada = $"{nomeSemExtensao}_inteira";

            // Enviar imagem reduzida
            var retornoReduzida = await _imgbbService
                .UploadImage(new ImgbbUploadInputModel(ConverterBytesEmBase64(imagemReduzida, extensaoCompleta), nomeReduzida));

            // Enviar imagem inteira
            var retornoMaximizada = await _imgbbService
                .UploadImage(new ImgbbUploadInputModel(ConverterBytesEmBase64(imagemMaximizada, extensaoCompleta), nomeMaximizada));

            var imagem = new TesteImagem(nomeMaximizada, nomeReduzida, retornoMaximizada.Data.data.Url, retornoReduzida.Data.data.Url);

            _testeRepository.AdicionarTesteImagem(imagem);

            return await _testeRepository.UnitOfWork.Commit();
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

        private string ConverterBytesEmBase64(byte[] bytes, string extensaoImagem) =>
            Convert.ToBase64String(bytes, 0, bytes.Length);

        private bool ValidarComando(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
