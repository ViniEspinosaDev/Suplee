using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suplee.Api.Controllers.Identidade.InputModel;
using Suplee.Api.Controllers.Identidade.InputModels;
using Suplee.Api.Controllers.Identidade.ViewModels;
using Suplee.Api.Controllers.Vendas.ViewModels;
using Suplee.Catalogo.Api.Controllers;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Vendas.Domain.Interfaces;
using System;
using System.Linq;
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
        private readonly IPedidoRepository _pedidoRepository;

        public IdentidadeController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IUsuarioLogado usuario,
            IMapper mapper,
            IUsuarioRepository usuarioRepository,
            IPedidoRepository pedidoRepository) : base(notifications, mediatorHandler, usuario)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
            _pedidoRepository = pedidoRepository;
        }

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

        [HttpPut("editar-usuario")]
        public async Task<ActionResult> EditarConta(EditarUsuarioInputModel editarUsuario)
        {
            var comando = _mapper.Map<EditarUsuarioCommand>(editarUsuario);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Usuário atualizado com sucesso");
        }

        [AllowAnonymous]
        [HttpPost("confirmar-cadastro/{usuarioId}/{codigoConfirmacao}")]
        public async Task<ActionResult> ConfirmarCadastro(Guid usuarioId, string codigoConfirmacao)
        {
            var comando = new ConfirmarCadastroCommand(usuarioId, codigoConfirmacao);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Sua conta foi criada com sucesso");
        }

        [AllowAnonymous]
        [HttpPost("reenviar-email-confirmar-cadastro/{cpf}")]
        public async Task<ActionResult> ReenviarEmailConfirmarCadastro(string cpf)
        {
            var comando = new ReenviarEmailConfirmarCadastroCommand(cpf);

            string email = await _mediatorHandler.EnviarComando(comando);

            return CustomResponse($@"Confirme seu cadastro no e-mail ""{email}""");
        }

        [AllowAnonymous]
        [HttpPost("recuperar-senha/{cpf}")]
        public async Task<ActionResult> RecuperarSenha(string cpf)
        {
            var comando = new RecuperarSenhaCommand(cpf);

            string email = await _mediatorHandler.EnviarComando(comando);

            return CustomResponse($@"Um e-mail de recuperação de senha foi enviado para ""{email}""");
        }

        [AllowAnonymous]
        [HttpPost("alterar-senha")]
        public async Task<ActionResult> AlterarSenha(AlterarSenhaInputModel alterarSenha)
        {
            var comando = _mapper.Map<AlterarSenhaCommand>(alterarSenha);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Senha alterada com sucesso");
        }

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

        [HttpPost("cadastrar-endereco")]
        public async Task<ActionResult> CadastrarEndereco(CadastrarEnderecoInputModel endereco)
        {
            var comando = _mapper.Map<CadastrarEnderecoCommand>(endereco);

            comando.VincularUsuarioId(UsuarioId);

            await _mediatorHandler.EnviarComando(comando);

            return CustomResponse("Endereço cadastrado com sucesso");
        }

        [HttpGet("recuperar-usuario-completo")]
        public async Task<ActionResult> RecuperarUsuarioCompleto()
        {
            var usuario = _usuarioRepository.ObterPeloId(UsuarioId);

            if (usuario == null)
            {
                NotificarErro("", "Não existe usuário para este Id");
                return await Task.FromResult(CustomResponse());
            }

            usuario.Atualizar(usuario.Nome, usuario.Celular, usuario.Enderecos.Where(x => x.EnderecoPadrao).ToList());

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            var pedidoRascunho = await _pedidoRepository.ObterCarrinhoPorUsuarioId(UsuarioId);

            if (pedidoRascunho == null)
                return CustomResponse(MapearUsuarioCompleto(usuarioViewModel, null));

            var carrinhoViewModel = _mapper.Map<PedidoViewModel>(pedidoRascunho);

            return CustomResponse(MapearUsuarioCompleto(usuarioViewModel, carrinhoViewModel));
        }

        private UsuarioCompletoViewModel MapearUsuarioCompleto(UsuarioViewModel usuario, PedidoViewModel carrinho)
        {
            return new UsuarioCompletoViewModel()
            {
                Usuario = usuario,
                Carrinho = carrinho
            };
        }
    }
}
