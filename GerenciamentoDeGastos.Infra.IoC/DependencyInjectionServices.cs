using GerenciamentoDeGastos.Application.Services;
using GerenciamentoDeGastos.Domain.Interfaces;
using GerenciamentoDeGastos.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoDeGastos.Infra.IoC
{
    public static class DependencyInjectionServices
    {
        public static void ConfigureServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<LoginService>();
            service.AddScoped<BankAccountService>();
            service.AddScoped<MovementService>();
        }
    }
}
