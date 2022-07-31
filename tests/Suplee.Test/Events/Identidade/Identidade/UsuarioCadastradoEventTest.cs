using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Messages.Mail;
using Suplee.Test.Builder.Events.Identidade.Identidade;
using Suplee.Test.Builder.Models;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Events.Identidade.Identidade
{
    public class UsuarioCadastradoEventTest : IdentidadeEventTestBase
    {
        [Fact]
        public async Task Deve_Nao_Salvar_Confirmacao_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var evento = new UsuarioCadastradoEventBuilder(usuario).Build();

            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(false));

            await _handler.Handle(evento, _cancellationToken);

            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "Não foi possível salvar confirmação de usuário")), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Deve_Enviar_Email()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            var evento = new UsuarioCadastradoEventBuilder(usuario).Build();

            _usuarioRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            await _handler.Handle(evento, _cancellationToken);

            _mailService.Verify(x => x.SendMailAsync(It.IsAny<Mail>()), Times.Once);
        }
    }
}
