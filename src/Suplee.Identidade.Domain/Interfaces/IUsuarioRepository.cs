using Suplee.Core.Data;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool ExisteUsuarioComCPF(string cpf);
        bool ExisteUsuarioComEmail(string email);

        Usuario RecuperarPeloCPF(string cpf);
        Usuario RecuperarPeloEmail(string email);

        void Adicionar(Usuario usuario);
    }
}
