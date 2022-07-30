using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using Suplee.Identidade.Domain.Tools;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class IdentidadeCommandHandler :
        CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, Usuario>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public IdentidadeCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(Usuario);
            }

            string cpf = new string(request.CPF.Where(x => x >= '0' && x <= '9').ToArray());

            bool cpfInvalido = string.IsNullOrEmpty(cpf) || cpf.Length != 11;

            if (cpfInvalido)
            {
                await NotificarErro(request, "O CPF é inválido");
                return default(Usuario);
            }

            var senhasDiferentes = request.Senha != request.ConfirmacaoSenha;

            if (senhasDiferentes)
            {
                await NotificarErro(request, "As senhas não são iguais");
                return default(Usuario);
            }

            var existeUsuarioComCPF = _usuarioRepository.ExisteUsuarioComCPF(cpf);

            if (existeUsuarioComCPF)
            {
                await NotificarErro(request, "Este CPF já está sendo usado por outro usuário");
                return default(Usuario);
            }

            var existeUsuarioComEmail = _usuarioRepository.ExisteUsuarioComEmail(request.Email);

            if (existeUsuarioComEmail)
            {
                await NotificarErro(request, "Este E-mail já está sendo usado por outro usuário");
                return default(Usuario);
            }

            var usuario = new Usuario(
                 request.Nome,
                 request.Email,
                 HashPassword.GenerateSHA512String(request.Senha),
                 cpf,
                 request.Celular,
                 ETipoUsuario.Normal,
                 EStatusUsuario.AguardandoConfirmacao);

            _usuarioRepository.Adicionar(usuario);

            var sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await NotificarErro(request, "Não foi possível cadastrar usuário");
                return default(Usuario);
            }

            await _mediatorHandler.PublicarDomainEvent(new UsuarioCadastradoEvent(usuario));

            return usuario;
        }
    }
}
