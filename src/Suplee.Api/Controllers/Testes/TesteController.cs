using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Testes.InputModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Messages.Mail;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Teste.Domain.Commands;
using Suplee.Teste.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Api.Controllers.Testes
{
    /// <summary>
    /// Controler para fazer testes
    /// </summary>
    public class TesteController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ITesteRepository _testeRepository;
        private readonly IMailService _mailService;
        /// <summary>
        /// Construtor controller de testes
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="usuario"></param>
        /// <param name="testeRepository"></param>
        /// <param name="mailService"></param>
        public TesteController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            ITesteRepository testeRepository,
            IMailService mailService) : base(notifications, mediatorHandler, usuario)
        {
            _mediatorHandler = mediatorHandler;
            _testeRepository = testeRepository;
            _mailService = mailService;
        }

        /// <summary>
        /// Cadastrar imagem enviando File 
        /// </summary>
        /// <param name="cadastrarImagem"></param>
        /// <returns></returns>
        [HttpPost("cadastrar-imagem")]
        public async Task<ActionResult> CadastrarImagem([FromForm] CadastrarImagemInputModel cadastrarImagem)
        {
            var comando = new CadastrarImagemCommand(cadastrarImagem.Imagem);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Imagem cadastrada com sucesso");
        }

        /// <summary>
        /// Cadastrar imagens enviando File 
        /// </summary>
        /// <param name="cadastrarImagem"></param>
        /// <returns></returns>
        [HttpPost("cadastrar-imagens")]
        //public ActionResult CadastrarImagens([FromForm] CadastrarImagensInputModel cadastrarImagem)
        public ActionResult CadastrarImagens([FromForm] ICollection<IFormFile> cadastrarImagem)
        {
            return CustomResponse("Imagens cadastradas com sucesso");
        }

        /// <summary>
        /// Recuperar todas imagens cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet("recuperar-imagens")]
        public async Task<ActionResult> RecuperarImagens()
        {
            var imagens = _testeRepository.ObterTodasImagens();

            return await Task.FromResult(CustomResponse(imagens));
        }

        /// <summary>
        /// Enviar e-mail
        /// </summary>
        [HttpPost("enviar-email/{enderecoEmail}")]
        public async Task<ActionResult> EnviarEmail(string enderecoEmail = "vini.espinosa1@gmail.com")
        {
            var email = new Mail(
                mailAddress: enderecoEmail,
                bodyText: $"<p>Para confirmar sua conta, clique no link abaixo: <a>/confirmar-cadastro/0040a58b-bd73-4dd7-a334-1d69c3570a6d/h9hLP3zC6o</a></p>",
                subject: "Confirmação de criação de conta na Suplee");

            await _mailService.SendMailAsync(email);

            return CustomResponse();
        }
    }
}
