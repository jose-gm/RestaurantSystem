using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Mesa");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Mesa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Cuenta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Mesa");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Cuenta");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Mesa",
                nullable: true);
        }
    }
}
