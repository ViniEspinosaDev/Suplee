using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Suplee.Api.Controllers.Identidade.InputModel;
using Suplee.Api.Controllers.Identidade.InputModels;
using Suplee.Api.Controllers.Identidade.ViewModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Suplee.Api.Controllers.Identidade
{
    [Authorize]
    [Route("api/[controller]")]
    public class IdentidadeController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public IdentidadeController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IOptions<ConfiguracaoAplicacao> appSettings,
            IMapper mapper,
            IUsuarioRepository usuarioRepository) : base(notifications, mediatorHandler, usuario)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Criar nova conta
        /// </summary>
        /// <param name="novaConta"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("cadastrar-usuario")]
        public async Task<ActionResult> CadastrarConta(CadastroUsuarioInputModel novaConta)
        {
            var comando = _mapper.Map<CadastrarUsuarioCommand>(novaConta);

            var usuario = await _mediatorHandler.EnviarComando(comando);

            if (usuario is null)
                return CustomResponse();

            return CustomResponse($@"Confirme seu cadastro no e-mail ""{novaConta.Email}""");
        }

        /// <summary>
        /// Editar informações do usuário
        /// </summary>
        /// <param name="editarUsuario"></param>
        /// <returns></returns>
        [HttpPut("editar-usuario")]
        public async Task<ActionResult> EditarConta(EditarUsuarioInputModel editarUsuario)
        {
            var comando = _mapper.Map<EditarUsuarioCommand>(editarUsuario);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Usuário atualizado com sucesso");
        }

        /// <summary>
        /// Confirmar cadastro de conta de usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="codigoConfirmacao"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("confirmar-cadastro/{usuarioId}/{codigoConfirmacao}")]
        public async Task<ActionResult> ConfirmarCadastro(Guid usuarioId, string codigoConfirmacao)
        {
            var comando = new ConfirmarCadastroCommand(usuarioId, codigoConfirmacao);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Sua conta foi criada com sucesso");
        }

        /// <summary>
        /// Reenviar e-mail de confirmação de conta para o usuário pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("reenviar-email-confirmar-cadastro/{cpf}")]
        public async Task<ActionResult> ReenviarEmailConfirmarCadastro(string cpf)
        {
            var comando = new ReenviarEmailConfirmarCadastroCommand(cpf);

            string email = await _mediatorHandler.EnviarComando(comando);

            return CustomResponse($@"Confirme seu cadastro no e-mail ""{email}""");
        }

        /// <summary>
        /// Recuperar senha pelo e-mail
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("recuperar-senha/{cpf}")]
        public async Task<ActionResult> RecuperarSenha(string cpf)
        {
            var comando = new RecuperarSenhaCommand(cpf);

            string email = await _mediatorHandler.EnviarComando(comando);

            return CustomResponse($@"Um e-mail de recuperação de senha foi enviado para ""{email}""");
        }

        /// <summary>
        /// Alterar senha do usuário
        /// </summary>
        /// <param name="alterarSenha"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(AlterarSenhaInputModel alterarSenha)
        {
            var comando = _mapper.Map<AlterarSenhaCommand>(alterarSenha);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Senha alterada com sucesso");
        }

        /// <summary>
        /// Recuperar informações para edição de usuário
        /// </summary>
        /// <returns></returns>
        [HttpGet("recuperar-informacoes-usuario")]
        public async Task<ActionResult> RecuperarInformacoesUsuario()
        {
            var usuario = _usuarioRepository.ObterPeloId(UsuarioId);

            if (usuario == null)
            {
                NotificarErro("", "Não existe usuário para este Id");
                return await Task.FromResult(CustomResponse());
            }

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            return CustomResponse(usuarioViewModel);
        }

        // Recupear informações usuário pra edição

        // Recuperar todas informações usuário
        /*
         Informacoes básicas (nome, cpf, email, telefone)
        Endereço (tudo)
        Carrinho
         
         */


        // Criar coluna na tabela de endereços para verificar qual endereço padrão
    }
}
