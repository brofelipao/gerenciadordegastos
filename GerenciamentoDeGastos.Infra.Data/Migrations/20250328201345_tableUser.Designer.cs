﻿// <auto-generated />
using System;
using GerenciamentoDeGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GerenciamentoDeGastos.Infra.Data.Migrations
{
    [DbContext(typeof(ExpensesContext))]
    [Migration("20250328201345_tableUser")]
    partial class tableUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Balance")
                        .HasPrecision(10, 4)
                        .HasColumnType("decimal(10,4)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.Movement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(13, 4)
                        .HasColumnType("decimal(13,4)");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsRecurrent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("PersonId");

                    b.ToTable("Movement");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("int");

                    b.Property<int>("Sobrenome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastAccess")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.BankAccount", b =>
                {
                    b.HasOne("GerenciamentoDeGastos.Domain.Entities.Person", "Person")
                        .WithMany("BankAccounts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.Movement", b =>
                {
                    b.HasOne("GerenciamentoDeGastos.Domain.Entities.BankAccount", "BankAccount")
                        .WithMany("Movements")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciamentoDeGastos.Domain.Entities.Person", "Person")
                        .WithMany("Movements")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.User", b =>
                {
                    b.HasOne("GerenciamentoDeGastos.Domain.Entities.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("GerenciamentoDeGastos.Domain.Entities.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.BankAccount", b =>
                {
                    b.Navigation("Movements");
                });

            modelBuilder.Entity("GerenciamentoDeGastos.Domain.Entities.Person", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("Movements");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
