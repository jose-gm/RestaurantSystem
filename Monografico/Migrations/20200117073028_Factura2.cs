using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class Factura2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "Factura",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tarjeta",
                table: "Factura",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoTarjeta",
                table: "Factura",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "Factura");

            migrationBuilder.DropColumn(
                name: "Tarjeta",
                table: "Factura");

            migrationBuilder.DropColumn(
                name: "TipoTarjeta",
                table: "Factura");
        }
    }
}
