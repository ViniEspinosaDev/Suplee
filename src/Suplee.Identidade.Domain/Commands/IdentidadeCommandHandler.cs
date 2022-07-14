using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Commands
{
    public class IdentidadeCommandHandler :
        IRequestHandler<CadastrarUsuarioCommand, bool>,
        IRequestHandler<RealizarLoginCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public IdentidadeCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var senhasDiferentes = request.Senha != request.ConfirmacaoSenha;

            if (senhasDiferentes)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "As senhas não são iguais"));
                return false;
            }

            var existeUsuarioComCPF = _usuarioRepository.ExisteUsuarioComCPF(request.CPF);

            if (existeUsuarioComCPF)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Este CPF já está sendo usado por outro usuário"));
                return false;
            }

            var existeUsuarioComEmail = _usuarioRepository.ExisteUsuarioComEmail(request.Email);

            if (existeUsuarioComEmail)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Este E-mail já está sendo usado por outro usuário"));
                return false;
            }

            var usuario = new Usuario(
                 request.Nome,
                 request.Email,
                 request.Senha,
                 new string(request.CPF.Where(x => x >= '0' && x <= '9').ToArray()),
                 request.Celular,
                 ETipoUsuario.Normal);

            _usuarioRepository.Adicionar(usuario);

            var sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Não foi possível cadastrar usuário"));
                return false;
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioCadastradoEvent());

            return sucesso;
        }

        public async Task<bool> Handle(RealizarLoginCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var usuario = _usuarioRepository.RecuperarPeloEmail(request.Email);

            if (usuario is null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Não existe nenhum usuário cadastrado com esse e-mail"));
                return false;
            }

            // TODO: Descriptografar
            var senhasDiferentes = usuario.Senha != request.Senha;

            if (senhasDiferentes)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "E-mail e/ou senha inválidos"));
                return false;
            }

            return true;
        }

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
