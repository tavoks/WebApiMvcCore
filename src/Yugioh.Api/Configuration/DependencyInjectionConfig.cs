using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yugioh.Business.Intefaces;
using Yugioh.Business.Services;
using Yugioh.Data.Context;
using Yugioh.Data.Repository;

namespace Yugioh.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDepndencies(this IServiceCollection services)
        {
            services.AddScoped<ApiDbContext>();
            services.AddScoped<IDuelistaRepository, DuelistaRepository>();
            services.AddScoped<ICartaRepository, CartaRepository>();

            services.AddScoped<IDuelistaService, DuelistaService>();
            services.AddScoped<ICartaService, CartaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}
