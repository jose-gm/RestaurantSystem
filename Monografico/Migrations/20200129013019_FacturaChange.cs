using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class FacturaChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cheque",
                table: "Factura",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cheque",
                table: "Factura");
        }
    }
}
