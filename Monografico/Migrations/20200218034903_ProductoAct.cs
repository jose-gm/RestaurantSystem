using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class ProductoAct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdItbis",
                table: "Producto",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdItbis",
                table: "Producto",
                column: "IdItbis");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Itbis_IdItbis",
                table: "Producto",
                column: "IdItbis",
                principalTable: "Itbis",
                principalColumn: "IdItbis",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Itbis_IdItbis",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_IdItbis",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "IdItbis",
                table: "Producto");
        }
    }
}
