using Suplee.Identidade.Data;
using Suplee.Identidade.Data.Repository;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Models;
using Suplee.Test.Context;
using System;
using Xunit;

namespace Suplee.Test.Repositories.Identidade
{
    public class UsuarioRepositoryTest
    {
        private readonly IdentidadeContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTest()
        {
            _context = StubContextDomain.GetDatabaseContextIdentidade();
            _usuarioRepository = new UsuarioRepository(_context);
        }

        [Fact]
        public void Deve_Testar_Instancia_Repository()
        {
            Assert.True(_usuarioRepository != null);
        }

        #region Usuario
        [Fact]
        public void Deve_Verificar_Se_Existe_Usuario_Com_CPF()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var existe = _usuarioRepository.ExisteUsuarioComCPF(usuario.CPF);

            Assert.True(existe);
        }

        [Fact]
        public void Deve_Nao_Verificar_Se_Existe_Usuario_Com_CPF()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var existe = _usuarioRepository.ExisteUsuarioComCPF("CPFErrado");

            Assert.False(existe);
        }

        [Fact]
        public void Deve_Verificar_Se_Existe_Usuario_Com_Email()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var existe = _usuarioRepository.ExisteUsuarioComEmail(usuario.Email);

            Assert.True(existe);
        }

        [Fact]
        public void Deve_Nao_Verificar_Se_Existe_Usuario_Com_Email()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var existe = _usuarioRepository.ExisteUsuarioComEmail("EmailErrado");

            Assert.False(existe);
        }

        [Fact]
        public void Deve_Obter_Usuario_Pelo_Id()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.ObterPeloId(usuario.Id);

            Assert.NotNull(usuarioAdicionado);
            Assert.NotNull(usuarioAdicionado.Enderecos);
            Assert.Single(usuarioAdicionado.Enderecos);
        }

        [Fact]
        public void Deve_Nao_Obter_Usuario_Pelo_Id()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.ObterPeloId(Guid.Empty);

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Obter_Usuario_Pelo_CPF()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.ObterPeloCPF(usuario.CPF);

            Assert.NotNull(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Nao_Obter_Usuario_Pelo_CPF()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.ObterPeloCPF("CPFErrado");

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Obter_Usuario_Email_Senha()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginEmail(usuario.Email, usuario.Senha);

            Assert.NotNull(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Obter_Usuario_Email_Errado_Senha()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginEmail("EmailErrado", usuario.Senha);

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Obter_Usuario_Email_Senha_Errada()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginEmail(usuario.Email, "SenhaErrada");

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Obter_Usuario_CPF_Senha()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginCPF(usuario.CPF, usuario.Senha);

            Assert.NotNull(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Nao_Obter_Usuario_CPF_Errado_Senha()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginCPF("CPFErrado", usuario.Senha);

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Nao_Obter_Usuario_CPF_Senha_Errada()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.RealizarLoginCPF(usuario.CPF, "SenhaErrada");

            Assert.Null(usuarioAdicionado);
        }

        [Fact]
        public void Deve_Adicionar_Usuario()
        {
            var usuario = new UsuarioBuilder().PadraoValido().Build();

            AdicionarUsuario(usuario);

            var usuarioAdicionado = _usuarioRepository.ObterPeloId(usuario.Id);

            Assert.NotNull(usuarioAdicionado);
        }
        #endregion

        #region ConfirmacaoUsuario
        [Fact]
        public void Deve_Obter_Confirmacao_Usuario()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuario(confirmacaoUsuario.UsuarioId, confirmacaoUsuario.CodigoConfirmacao);

            Assert.NotNull(confirmacaoUsuarioAdicionada);
        }

        [Fact]
        public void Deve_Nao_Obter_Confirmacao_Usuario_Errado()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuario(Guid.NewGuid(), confirmacaoUsuario.CodigoConfirmacao);

            Assert.Null(confirmacaoUsuarioAdicionada);
        }

        [Fact]
        public void Deve_Nao_Obter_Confirmacao_Usuario_Codigo_Errado()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuario(confirmacaoUsuario.UsuarioId, "CodigoErrado");

            Assert.Null(confirmacaoUsuarioAdicionada);
        }

        [Fact]
        public void Deve_Obter_Confirmacao_Usuario_Ainda_Nao_Confirmada()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuarioAindaNaoConfirmada(confirmacaoUsuario.UsuarioId);

            Assert.NotNull(confirmacaoUsuarioAdicionada);
        }

        [Fact]
        public void Deve_Nao_Obter_Confirmacao_Usuario_Ainda_Nao_Confirmada()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            confirmacaoUsuario.Confirmar();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuarioAindaNaoConfirmada(confirmacaoUsuario.UsuarioId);

            Assert.Null(confirmacaoUsuarioAdicionada);
        }

        [Fact]
        public void Deve_Adicionar_Confirmacao_Usuario()
        {
            var confirmacaoUsuario = new ConfirmacaoUsuarioBuilder().PadraoValido().Build();

            AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            var confirmacaoUsuarioAdicionada = _usuarioRepository.ObterConfirmacaoUsuario(confirmacaoUsuario.UsuarioId, confirmacaoUsuario.CodigoConfirmacao);

            Assert.NotNull(confirmacaoUsuarioAdicionada);
        }
        #endregion

        #region Métodos auxiliares
        private void AdicionarUsuario(Usuario usuario)
        {
            _usuarioRepository.Adicionar(usuario);

            _usuarioRepository.UnitOfWork.Commit();
        }

        private void AdicionarConfirmacaoUsuario(ConfirmacaoUsuario confirmacaoUsuario)
        {
            _usuarioRepository.AdicionarConfirmacaoUsuario(confirmacaoUsuario);

            _usuarioRepository.UnitOfWork.Commit();
        }
        #endregion
    }
}
