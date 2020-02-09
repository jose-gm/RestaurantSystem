using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class ProductoChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Desactivado",
                table: "Producto",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desactivado",
                table: "Producto");
        }
    }
}
