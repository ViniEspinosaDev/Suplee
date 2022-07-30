using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Commands
{
    public class IdentidadeCommandHandler :
        CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, Usuario>,
        IRequestHandler<RealizarLoginEmailCommand, Usuario>,
        IRequestHandler<RealizarLoginCPFCommand, Usuario>
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
                 request.Senha,
                 cpf,
                 request.Celular,
                 ETipoUsuario.Normal);

            _usuarioRepository.Adicionar(usuario);

            var sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await NotificarErro(request, "Não foi possível cadastrar usuário");
                return default(Usuario);
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioCadastradoEvent());

            return usuario;
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

            var usuario = _usuarioRepository.RealizarLoginEmail(request.Email, request.Senha);

            if (usuario is null)
            {
                await NotificarErro(request, "E-mail e/ou senha inválidos");
                return default(Usuario);
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioCadastradoEvent());

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

            var usuario = _usuarioRepository.RealizarLoginCPF(request.CPF, request.Senha);

            if (usuario is null)
            {
                await NotificarErro(request, "CPF e/ou senha inválidos");
                return default(Usuario);
            }

            // TODO: Futuramente lançar evento de propagação para banco de dados de leitura
            //await _mediatorHandler.PublicarEvento(new UsuarioCadastradoEvent());

            return usuario;
        }
    }
}
