﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Suplee.Api.Controllers.Identidade.InputModels;
using Suplee.Api.Controllers.Identidade.ViewModels;
using Suplee.Api.Extensions;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Suplee.Api.Controllers.Identidade
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AutenticacaoController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ConfiguracaoAplicacao _configuracaoAplicacao;

        public AutenticacaoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IOptions<ConfiguracaoAplicacao> appSettings,
            IMapper mapper) : base(notifications, mediatorHandler, usuario)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _configuracaoAplicacao = appSettings.Value;
        }

        [HttpPost("login-email")]
        public async Task<ActionResult> FazerLoginEmail(LoginEmailInputModel loginInputModel)
        {
            var comando = _mapper.Map<RealizarLoginEmailCommand>(loginInputModel);

            var usuario = await _mediatorHandler.EnviarComando(comando);

            if (usuario is null)
                return CustomResponse();

            return CustomResponse(GerarJwt(usuario));
        }

        [HttpPost("login-cpf")]
        public async Task<ActionResult> FazerLoginCPF(LoginCPFInputModel loginInputModel)
        {
            var comando = _mapper.Map<RealizarLoginCPFCommand>(loginInputModel);

            var usuario = await _mediatorHandler.EnviarComando(comando);

            if (usuario is null)
                return CustomResponse();

            return CustomResponse(GerarJwt(usuario));
        }

        private LoginViewModel GerarJwt(Usuario usuario)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypeExtension.UsuarioId, usuario.Id.ToString()));
            claims.Add(new Claim(ClaimTypeExtension.Nome, usuario.Nome));
            claims.Add(new Claim(ClaimTypeExtension.Email, usuario.Email));
            claims.Add(new Claim(ClaimTypeExtension.TipoUsuario, usuario.Tipo.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuracaoAplicacao.Segredo);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuracaoAplicacao.Emissor,
                Audience = _configuracaoAplicacao.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_configuracaoAplicacao.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_configuracaoAplicacao.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    UsuarioId = usuario.Id.ToString(),
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = usuario.Tipo.ToString()
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
