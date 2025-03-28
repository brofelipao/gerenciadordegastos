
using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeGastos.Infra.Data.Context
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options)
            : base(options)
        {
        }

        public virtual Person Persons { get; set; }
        public virtual Movement Movements { get; set; }
        public virtual BankAccount BankAccounts { get; set; }
        public virtual User Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new MovementConfiguration());
            builder.ApplyConfiguration(new BankAccountConfiguration());
        }
    }
}
