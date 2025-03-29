using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Interfaces;
using GerenciamentoDeGastos.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoDeGastos.Infra.IoC
{
    public static class DependencyInjectionRepositories
    {
        public static void ConfigureRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IBankAccountRepository, BankAccountRepository>();
        }
    }
}
