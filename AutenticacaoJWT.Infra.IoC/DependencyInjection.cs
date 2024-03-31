using AutenticacaoJWT.Application.AutoMapper;
using AutenticacaoJWT.Application.Interfaces;
using AutenticacaoJWT.Application.Services;
using AutenticacaoJWT.Domain.Interfaces;
using AutenticacaoJWT.Infra.Data.Context;
using AutenticacaoJWT.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {

            // Adicionando o contexto do banco de dados usando o provedor de serviços personalizado
            services.AddDbContext<ContextApp>(options => options.UseNpgsql(configuration.GetConnectionString("AutenticacaoJWTDB")));
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            
            return services;
        }
    }
}
