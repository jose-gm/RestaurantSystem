using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Ingrediente_IdIngrediente",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Producto_IdProducto",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoria_IdCategoria",
                table: "Producto");

            migrationBuilder.AlterColumn<int>(
                name: "IdCategoria",
                table: "Producto",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Ingrediente_IdIngrediente",
                table: "Inventario",
                column: "IdIngrediente",
                principalTable: "Ingrediente",
                principalColumn: "IdIngrediente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Producto_IdProducto",
                table: "Inventario",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoria_IdCategoria",
                table: "Producto",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Ingrediente_IdIngrediente",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Producto_IdProducto",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoria_IdCategoria",
                table: "Producto");

            migrationBuilder.AlterColumn<int>(
                name: "IdCategoria",
                table: "Producto",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Ingrediente_IdIngrediente",
                table: "Inventario",
                column: "IdIngrediente",
                principalTable: "Ingrediente",
                principalColumn: "IdIngrediente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Producto_IdProducto",
                table: "Inventario",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoria_IdCategoria",
                table: "Producto",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
