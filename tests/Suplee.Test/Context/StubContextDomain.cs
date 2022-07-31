using Microsoft.EntityFrameworkCore;
using Suplee.Identidade.Data;
using System;

namespace Suplee.Test.Context
{
    public class StubContextDomain
    {
        public static IdentidadeContext GetDatabaseContextIdentidade()
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentidadeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;

            return new IdentidadeContext(options);
        }
    }
}
