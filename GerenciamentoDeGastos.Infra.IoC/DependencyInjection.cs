using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GerenciamentoDeGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeGastos.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection Configure(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            service.AddDbContext<ExpensesContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            return service;
        }
    }
}
