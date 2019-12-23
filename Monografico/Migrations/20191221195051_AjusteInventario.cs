using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class AjusteInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AjusteInventario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdInventario = table.Column<int>(nullable: false),
                    CantidadAnterior = table.Column<int>(nullable: false),
                    CantidadActual = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjusteInventario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdMesa",
                table: "Cuenta",
                column: "IdMesa");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Mesa_IdMesa",
                table: "Cuenta",
                column: "IdMesa",
                principalTable: "Mesa",
                principalColumn: "IdMesa",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Mesa_IdMesa",
                table: "Cuenta");

            migrationBuilder.DropTable(
                name: "AjusteInventario");

            migrationBuilder.DropIndex(
                name: "IX_Cuenta_IdMesa",
                table: "Cuenta");
        }
    }
}
