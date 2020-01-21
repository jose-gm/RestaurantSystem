using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class CajaChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContadoCaja",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "Tarjeta",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "IdCierreCaja",
                table: "Caja");

            migrationBuilder.AddColumn<string>(
                name: "Cuadre",
                table: "CierreCaja",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCaja",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CierreCaja_IdCaja",
                table: "CierreCaja",
                column: "IdCaja",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CierreCaja_Caja_IdCaja",
                table: "CierreCaja",
                column: "IdCaja",
                principalTable: "Caja",
                principalColumn: "IdCaja",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CierreCaja_Caja_IdCaja",
                table: "CierreCaja");

            migrationBuilder.DropIndex(
                name: "IX_CierreCaja_IdCaja",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "Cuadre",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "IdCaja",
                table: "CierreCaja");

            migrationBuilder.AddColumn<decimal>(
                name: "ContadoCaja",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tarjeta",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdCierreCaja",
                table: "Caja",
                nullable: false,
                defaultValue: 0);
        }
    }
}
