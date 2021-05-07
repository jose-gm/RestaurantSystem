using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monografico.Migrations
{
    public partial class Caja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    IdCaja = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCierreCaja = table.Column<int>(nullable: false),
                    IdUsario = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Monto = table.Column<decimal>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.IdCaja);
                });

            migrationBuilder.CreateTable(
                name: "CierreCaja",
                columns: table => new
                {
                    IdCierreCaja = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MontoCaja = table.Column<decimal>(nullable: false),
                    ContadoCaja = table.Column<decimal>(nullable: false),
                    TotalContado = table.Column<decimal>(nullable: false),
                    Tarjeta = table.Column<decimal>(nullable: false),
                    Diferencia = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierreCaja", x => x.IdCierreCaja);
                });

            migrationBuilder.CreateTable(
                name: "AperturaCaja",
                columns: table => new
                {
                    IdAperturaCaja = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCaja = table.Column<int>(nullable: false),
                    IdUsurio = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MontoInicial = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AperturaCaja", x => x.IdAperturaCaja);
                    table.ForeignKey(
                        name: "FK_AperturaCaja_Caja_IdCaja",
                        column: x => x.IdCaja,
                        principalTable: "Caja",
                        principalColumn: "IdCaja",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AperturaCaja_IdCaja",
                table: "AperturaCaja",
                column: "IdCaja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AperturaCaja");

            migrationBuilder.DropTable(
                name: "CierreCaja");

            migrationBuilder.DropTable(
                name: "Caja");
        }
    }
}
