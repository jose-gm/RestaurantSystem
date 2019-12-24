using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class AjusteEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "AjusteInventario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AjusteInventario");
        }
    }
}
