using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using Suplee.Identidade.Domain.Tools;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Autenticacao.Commands
{
    public class AutenticacaoCommandHandler :
        CommandHandler,
        IRequestHandler<RealizarLoginEmailCommand, Usuario>,
        IRequestHandler<RealizarLoginCPFCommand, Usuario>,
        IRequestHandler<ConfirmarCadastroCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(RealizarLoginEmailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(Usuario);
            }

            var existeUsuario = _usuarioRepository.ExisteUsuarioComEmail(request.Email);

            if (!existeUsuario)
            {
                await NotificarErro(request, "Não existe nenhum usuário cadastrado com esse e-mail");
                return default(Usuario);
            }

            var usuario = _usuarioRepository.RealizarLoginEmail(request.Email, HashPassword.GenerateSHA512String(request.Senha));

            if (usuario is null)
            {
                await NotificarErro(request, "E-mail e/ou senha inválidos");
                return default(Usuario);
            }

            if (usuario.Status == EStatusUsuario.AguardandoConfirmacao)
            {
                await NotificarErro(request, $@"Confirme a criação de sua conta pelo e-mail ""{usuario.Email}"" antes de fazer o login");
                return default(Usuario);
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioLogadoEvent());

            return usuario;
        }

        public async Task<Usuario> Handle(RealizarLoginCPFCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(Usuario);
            }

            var existeUsuario = _usuarioRepository.ExisteUsuarioComCPF(request.CPF);

            if (!existeUsuario)
            {
                await NotificarErro(request, "Não existe nenhum usuário cadastrado com esse cpf");
                return default(Usuario);
            }

            var usuario = _usuarioRepository.RealizarLoginCPF(request.CPF, HashPassword.GenerateSHA512String(request.Senha));

            if (usuario is null)
            {
                await NotificarErro(request, "CPF e/ou senha inválidos");
                return default(Usuario);
            }

            if (usuario.Status == EStatusUsuario.AguardandoConfirmacao)
            {
                await NotificarErro(request, $@"Confirme a criação de sua conta pelo e-mail ""{usuario.Email}"" antes de fazer o login");
                return default(Usuario);
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioLogadoEvent());

            return usuario;
        }

        public async Task<bool> Handle(ConfirmarCadastroCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return false;
            }

            var usuario = _usuarioRepository.ObterPeloId(request.UsuarioId);

            if (usuario is null)
            {
                await NotificarErro(request, "Não existe nenhum usuário cadastrado com esse Id");
                return false;
            }

            if (usuario.Status == EStatusUsuario.Ativo)
            {
                await NotificarErro(request, "Esse usuário já está ativo. Tente realizar o login");
                return false;
            }

            var confirmacaoUsuario = _usuarioRepository.ObterConfirmacaoUsuario(request.UsuarioId, request.CodigoConfirmacao);

            if (confirmacaoUsuario is null)
            {
                await NotificarErro(request, "Usuário e/ou código de confirmação inválidos");
                return false;
            }

            if (confirmacaoUsuario.Confirmado())
            {
                await NotificarErro(request, "Este código de confirmação já foi confirmado");
                return false;
            }

            confirmacaoUsuario.Confirmar();
            usuario.Ativar();

            return await _usuarioRepository.UnitOfWork.Commit();
        }
    }
}
