using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Messages.Mail;
using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Identidade.Events
{
    public class IdentidadeEventHandler :
        INotificationHandler<UsuarioCadastradoEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMailService _mailService;

        public IdentidadeEventHandler(
            IMediatorHandler mediatorHandler, 
            IUsuarioRepository usuarioRepository, 
            IMailService mailService)
        {
            _usuarioRepository = usuarioRepository;
            _mediatorHandler = mediatorHandler;
            _mailService = mailService;
        }

        public async Task Handle(UsuarioCadastradoEvent notification, CancellationToken cancellationToken)
        {
            string codigoConfirmacao = HashPassword.GenerateRandomCode(10);

            var confirmacaoUsuario = new ConfirmacaoUsuario(notification.Usuario.Id, codigoConfirmacao);

            _usuarioRepository.AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var resultado = await _usuarioRepository.UnitOfWork.Commit();

            if (!resultado)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("", "Não foi possível salvar confirmação de usuário"));
                return;
            }

            var email = new Mail(
                mailAddress: notification.Usuario.Email,
                bodyText: $@"<p>Para confirmar sua conta, clique no link abaixo: 
                                <a href=""https://suplee.vercel.app/confirmar-cadastro?usuarioId={notification.Usuario.Id}&codigoConfirmacao={codigoConfirmacao}"">
                                <br>UsuarioId: {notification.Usuario.Id}
                                <br>CodigoConfirmacao: {codigoConfirmacao}</a></p>",
                subject: "Confirmação de criação de conta na Suplee");

            await _mailService.SendMailAsync(email);
        }
    }
}
