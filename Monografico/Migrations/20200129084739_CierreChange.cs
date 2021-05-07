using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class CierreChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AperturaCaja_IdCaja",
                table: "AperturaCaja");

            migrationBuilder.AddColumn<decimal>(
                name: "Cheque",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Efectivo",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tarjeta",
                table: "CierreCaja",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_IdProducto",
                table: "FacturaDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_AperturaCaja_IdCaja",
                table: "AperturaCaja",
                column: "IdCaja",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaDetalle_Producto_IdProducto",
                table: "FacturaDetalle",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturaDetalle_Producto_IdProducto",
                table: "FacturaDetalle");

            migrationBuilder.DropIndex(
                name: "IX_FacturaDetalle_IdProducto",
                table: "FacturaDetalle");

            migrationBuilder.DropIndex(
                name: "IX_AperturaCaja_IdCaja",
                table: "AperturaCaja");

            migrationBuilder.DropColumn(
                name: "Cheque",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "Efectivo",
                table: "CierreCaja");

            migrationBuilder.DropColumn(
                name: "Tarjeta",
                table: "CierreCaja");

            migrationBuilder.CreateIndex(
                name: "IX_AperturaCaja_IdCaja",
                table: "AperturaCaja",
                column: "IdCaja");
        }
    }
}
