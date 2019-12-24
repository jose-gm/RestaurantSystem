using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class AjusteCambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "AjusteInventario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "AjusteInventario");
        }
    }
}
