using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Testes.InputModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Teste.Domain.Commands;
using Suplee.Teste.Domain.Interfaces;
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

        /// <summary>
        /// Construtor controller de testes
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="usuario"></param>
        /// <param name="testeRepository"></param>
        public TesteController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            ITesteRepository testeRepository) : base(notifications, mediatorHandler, usuario)
        {
            _mediatorHandler = mediatorHandler;
            _testeRepository = testeRepository;
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
        /// Recuperar todas imagens cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet("recuperar-imagens")]
        public async Task<ActionResult> RecuperarImagens()
        {
            var imagens = _testeRepository.ObterTodasImagens();

            return await Task.FromResult(CustomResponse(imagens));
        }
    }
}
