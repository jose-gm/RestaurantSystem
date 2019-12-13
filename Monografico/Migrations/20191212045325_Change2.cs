using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class Change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enviado",
                table: "Orden",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Cuenta",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enviado",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Cuenta");
        }
    }
}
