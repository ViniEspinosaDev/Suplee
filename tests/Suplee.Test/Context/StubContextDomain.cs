﻿using Microsoft.EntityFrameworkCore;
using Suplee.Catalogo.Data;
using Suplee.Identidade.Data;
using Suplee.Vendas.Data;
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

        public static CatalogoContext GetDatabaseContextCatalogo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;

            return new CatalogoContext(options);
        }

        public static VendasContext GetDatabaseContextVendas()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VendasContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;

            return new VendasContext(options);
        }
    }
}
