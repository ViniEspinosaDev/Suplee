using Microsoft.EntityFrameworkCore;
using Suplee.Catalogo.Data;
using System;

namespace Suplee.Test.Repositories.Catalogo
{
    public class CatalogoRepositoryBase
    {
        protected readonly CatalogoContext DbContext;

        public CatalogoRepositoryBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;

            DbContext = new CatalogoContext(options);

            _ = DbContext.Database.EnsureCreated();
        }

        public static DbContext GetDbContext() => new CatalogoRepositoryBase().DbContext;
    }
}
