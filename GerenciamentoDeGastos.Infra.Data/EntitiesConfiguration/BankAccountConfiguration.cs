using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeGastos.Infra.Data.EntitiesConfiguration
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.BankName).HasMaxLength(200);
            builder.Property(p => p.Agency).HasMaxLength(20);
            builder.Property(p => p.Account).HasMaxLength(20);
            builder.Property(p => p.Balance).HasPrecision(10, 4);

            builder.HasOne(p => p.Person).WithMany(x => x.BankAccounts).HasForeignKey(x => x.PersonId);  
        }

    }
}

