using Suplee.Test.Builder.Commands.Identidade.Identidade;
using Xunit;

namespace Suplee.Test.Commands.Identidade.Identidade
{
    public class RecuperarSenhaCommandTest : IdentidadeCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new RecuperarSenhaCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new RecuperarSenhaCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(2, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoCPF"));
        }
        #endregion

        #region Validações de negócios

        #endregion
    }
}
