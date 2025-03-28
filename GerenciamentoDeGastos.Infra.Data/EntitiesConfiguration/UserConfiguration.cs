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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Login).HasMaxLength(30);
            builder.Property(p => p.Password).HasMaxLength(30);
            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne(x => x.Person).WithOne(x => x.User).HasForeignKey<User>(x => x.Id);
        }
    }
}
