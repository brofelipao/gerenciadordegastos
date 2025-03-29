using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeGastos.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class movementChildrens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInvoiced",
                table: "Movement",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MovementIdFather",
                table: "Movement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movement_MovementIdFather",
                table: "Movement",
                column: "MovementIdFather");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Movement_MovementIdFather",
                table: "Movement",
                column: "MovementIdFather",
                principalTable: "Movement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Movement_MovementIdFather",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Movement_MovementIdFather",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "IsInvoiced",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "MovementIdFather",
                table: "Movement");
        }
    }
}
