using Suplee.Core.Data;
using Suplee.Teste.Domain.Interfaces;
using Suplee.Teste.Domain.Models;
using System.Collections.Generic;

namespace Suplee.Teste.Data.Repository
{
    public class TesteRepository : ITesteRepository
    {
        private readonly TesteContext _testeContext;

        public TesteRepository(TesteContext testeContext)
        {
            _testeContext = testeContext;
        }

        public IUnitOfWork UnitOfWork => _testeContext;

        public IEnumerable<TesteImagem> ObterTodasImagens()
        {
            return _testeContext.TesteImagem;
        }

        public void AdicionarTesteImagem(TesteImagem testeImagem)
        {
            _testeContext.Add(testeImagem);
        }

        public void Dispose()
        {
            _testeContext?.Dispose();
        }
    }
}
