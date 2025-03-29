using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeGastos.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeNameTablePerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "Person",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Person",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Person",
                newName: "Birthday");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccess",
                table: "User",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Person",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Person",
                newName: "Sobrenome");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Person",
                newName: "DataNascimento");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccess",
                table: "User",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
