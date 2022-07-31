﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Suplee.Api.Controllers.Identidade.InputModel;
using Suplee.Api.Controllers.Identidade.InputModels;
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
    /// <summary>
    /// Endpoints de identidade
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class IdentidadeController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        /// <summary>
        /// Construtor de identidade
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="usuario"></param>
        /// <param name="appSettings"></param>
        /// <param name="mapper"></param>
        public IdentidadeController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IOptions<ConfiguracaoAplicacao> appSettings,
            IMapper mapper) : base(notifications, mediatorHandler, usuario)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Criar nova conta
        /// </summary>
        /// <param name="novaConta"></param>
        /// <returns></returns>
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
        /// Confirmar cadastro de conta de usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="codigoConfirmacao"></param>
        /// <returns></returns>
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
        [HttpPost("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(AlterarSenhaInputModel alterarSenha)
        {
            var comando = _mapper.Map<AlterarSenhaCommand>(alterarSenha);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Senha alterada com sucesso");
        }
    }
}
