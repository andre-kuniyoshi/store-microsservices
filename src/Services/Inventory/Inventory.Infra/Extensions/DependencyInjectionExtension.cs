﻿using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Repositories;
using Inventory.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, PostgresContext>();
            services.AddScoped<ICouponRepository, CouponRepository>();

            return services;
        }
    }
}
