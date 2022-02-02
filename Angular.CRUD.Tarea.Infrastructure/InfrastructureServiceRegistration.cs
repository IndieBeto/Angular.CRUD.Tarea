using Angular.CRUD.Tarea.Application.Interfaces;
using Angular.CRUD.Tarea.Infrastructure.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Angular.CRUD.Tarea.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            return services;
        }
    }
}
