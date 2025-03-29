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
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {

        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Amount).HasPrecision(13, 4);
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            builder.Property(p => p.IsInvoiced).HasDefaultValue(false);
            builder.Property(p => p.IsRecurrent).HasDefaultValue(false);
            builder.Property(p => p.Description).HasMaxLength(100);

            builder.HasOne(x => x.Person).WithMany(x => x.Movements).HasForeignKey(x => x.PersonId);
            builder.HasOne(x => x.MovementFather).WithMany(x => x.MovementChildren).HasForeignKey(x => x.MovementIdFather);
            builder.HasOne(x => x.BankAccount).WithMany(x => x.Movements).HasForeignKey(x => x.BankAccountId);
        }
    }
}
