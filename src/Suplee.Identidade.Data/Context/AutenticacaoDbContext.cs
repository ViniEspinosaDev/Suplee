using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Suplee.Identidade.Data.Context
{
    public class AutenticacaoDbContext : IdentityDbContext
    {
        public AutenticacaoDbContext(DbContextOptions<AutenticacaoDbContext> options) : base(options)
        {
        }
    }
}
