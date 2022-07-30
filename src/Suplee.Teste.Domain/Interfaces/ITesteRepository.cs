using Suplee.Core.Data;
using Suplee.Teste.Domain.Models;
using System.Collections.Generic;

namespace Suplee.Teste.Domain.Interfaces
{
    public interface ITesteRepository : IRepository<TesteImagem>
    {
        IEnumerable<TesteImagem> ObterTodasImagens();
        void AdicionarTesteImagem(TesteImagem testeImagem);
    }
}
