using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.Mail;
using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class IdentidadeCommandHandler :
        CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, Usuario>,
        IRequestHandler<ReenviarEmailConfirmarCadastroCommand, string>,
        IRequestHandler<RecuperarSenhaCommand, string>,
        IRequestHandler<AlterarSenhaCommand, bool>,
        IRequestHandler<EditarUsuarioCommand, bool>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<ExcluirEnderecoCommand, bool>,
        IRequestHandler<MarcarEnderecoComoPadraoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailService _mailService;

        public IdentidadeCommandHandler(
            IMediatorHandler mediatorHandler,
            IUsuarioRepository usuarioRepository,
            IMailService mailService) : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
            _mailService = mailService;
        }

        public async Task<Usuario> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(Usuario);
            }

            var senhasDiferentes = request.Senha != request.ConfirmacaoSenha;

            if (senhasDiferentes)
            {
                await NotificarErro(request, "As senhas não são iguais");
                return default(Usuario);
            }

            var existeUsuarioComCPF = _usuarioRepository.ExisteUsuarioComCPF(request.CPF);

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
                 request.CPF,
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

        public async Task<string> Handle(ReenviarEmailConfirmarCadastroCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return string.Empty;
            }

            var usuario = _usuarioRepository.ObterPeloCPF(request.CPF);

            if (usuario is null)
            {
                await NotificarErro(request, "Não existe nenhum usuário cadastrado com esse CPF");
                return string.Empty;
            }

            if (usuario.Status == EStatusUsuario.Ativo)
            {
                await NotificarErro(request, "Esse usuário já está ativo. Tente realizar o login");
                return string.Empty;
            }

            var confirmacaoUsuario = _usuarioRepository.ObterConfirmacaoUsuarioAindaNaoConfirmada(usuario.Id);

            if (confirmacaoUsuario is null)
            {
                await NotificarErro(request, "Não existe nenhuma confirmação pendente para o usuário com esse CPF");
                return string.Empty;
            }

            string emailDestino = usuario.Email;

            var email = new Mail(
                mailAddress: emailDestino,
                bodyText: $@"<p>Para confirmar sua conta, clique no link abaixo: 
                                <a href=""https://suplee.vercel.app/confirmar-cadastro?usuarioId={usuario.Id}&codigoConfirmacao={confirmacaoUsuario.CodigoConfirmacao}"">
                                <br>UsuarioId: {usuario.Id}
                                <br>CodigoConfirmacao: {confirmacaoUsuario.CodigoConfirmacao}</a></p>",
                subject: "Confirmação de criação de conta na Suplee");

            await _mailService.SendMailAsync(email);

            return emailDestino;
        }

        public async Task<string> Handle(RecuperarSenhaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return string.Empty;
            }

            var usuario = _usuarioRepository.ObterPeloCPF(request.CPF);

            if (usuario is null)
            {
                await NotificarErro(request, "Não existe nenhum usuário cadastrado com esse CPF");
                return string.Empty;
            }

            if (usuario.Status == EStatusUsuario.AguardandoConfirmacao)
            {
                await NotificarErro(request, "O Usuário ainda não está com o cadastro confirmado. Verifique o e-mail");
                return string.Empty;
            }

            var confirmacaoUsuario = _usuarioRepository.ObterConfirmacaoUsuarioAindaNaoConfirmada(usuario.Id);

            if (confirmacaoUsuario is null)
            {
                confirmacaoUsuario = new ConfirmacaoUsuario(usuario.Id, HashPassword.GenerateRandomCode(10));
                _usuarioRepository.AdicionarConfirmacaoUsuario(confirmacaoUsuario);
                await _usuarioRepository.UnitOfWork.Commit();
            }

            string emailDestino = usuario.Email;

            var email = new Mail(
               mailAddress: emailDestino,
               bodyText: $@"<p>Para alterar sua senha, clique no link abaixo: 
                                <a href=""https://suplee.vercel.app/alterar-senha?usuarioId={usuario.Id}&codigoConfirmacao={confirmacaoUsuario.CodigoConfirmacao}"">
                                <br>UsuarioId: {usuario.Id}
                                <br>CodigoConfirmacao: {confirmacaoUsuario.CodigoConfirmacao}</a></p>",
               subject: "Alteração de senha na Suplee");

            await _mailService.SendMailAsync(email);

            return emailDestino;
        }

        public async Task<bool> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
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

            if (usuario.Status == EStatusUsuario.AguardandoConfirmacao)
            {
                await NotificarErro(request, "O Usuário ainda não está com o cadastro confirmado. Verifique o e-mail");
                return false;
            }

            var confirmacaoUsuario = _usuarioRepository.ObterConfirmacaoUsuario(usuario.Id, request.CodigoConfirmacao);

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

            var senhasDiferentes = request.Senha != request.ConfirmacaoSenha;

            if (senhasDiferentes)
            {
                await NotificarErro(request, "As senhas não são iguais");
                return false;
            }

            confirmacaoUsuario.Confirmar();
            usuario.AlterarSenha(HashPassword.GenerateSHA512String(request.Senha));

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
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

            usuario.Atualizar(request.Nome, request.Celular, request.Enderecos);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CadastrarEnderecoCommand request, CancellationToken cancellationToken)
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

            bool enderecoPadrao = request.EnderecoPadrao || usuario.Enderecos.Count == 0;

            if (enderecoPadrao)
                usuario.RemoverEnderecoPadrao();

            var endereco = new Endereco(
                usuarioId: usuario.Id,
                nomeDestinatario: request.NomeDestinatario,
                cep: request.CEP,
                estado: request.Estado,
                cidade: request.Cidade,
                bairro: request.Bairro,
                rua: request.Rua,
                numero: request.Numero,
                complemento: request.Complemento,
                tipoLocal: request.TipoLocal,
                telefone: request.Telefone,
                informacaoAdicional: request.InformacaoAdicional,
                enderecoPadrao: enderecoPadrao);

            _usuarioRepository.AdicionarEndereco(endereco);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ExcluirEnderecoCommand request, CancellationToken cancellationToken)
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

            if (!usuario.Enderecos.Any(x => x.Id == request.EnderecoId))
            {
                await NotificarErro(request, "Não existe nenhum endereço com id informado vinculado ao usuário");
                return false;
            }

            usuario.RemoverEndereco(request.EnderecoId);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(MarcarEnderecoComoPadraoCommand request, CancellationToken cancellationToken)
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

            if (!usuario.Enderecos.Any(x => x.Id == request.EnderecoId))
            {
                await NotificarErro(request, "Não existe nenhum endereço com id informado vinculado ao usuário");
                return false;
            }

            if (usuario.Enderecos.FirstOrDefault(x => x.EnderecoPadrao)?.Id == request.EnderecoId)
                return true;

            usuario.MarcarEnderecoPadrao(request.EnderecoId);

            return await _usuarioRepository.UnitOfWork.Commit();
        }
    }
}
