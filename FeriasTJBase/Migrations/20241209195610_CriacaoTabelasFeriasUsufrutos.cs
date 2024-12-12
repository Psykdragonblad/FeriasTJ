using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FeriasTJBase.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelasFeriasUsufrutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ferias",
                columns: table => new
                {
                    IdFerias = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matricula = table.Column<int>(type: "integer", nullable: false),
                    PeriodoAquisitivoInicial = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PeriodoAquisitivoFinal = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ferias", x => x.IdFerias);
                });

            migrationBuilder.CreateTable(
                name: "usufruto",
                columns: table => new
                {
                    IdUsufruto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdFerias = table.Column<int>(type: "integer", nullable: false),
                    UsufrutoInicial = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UsufrutoFinal = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usufruto", x => x.IdUsufruto);
                    table.ForeignKey(
                        name: "FK_usufruto_ferias_IdFerias",
                        column: x => x.IdFerias,
                        principalTable: "ferias",
                        principalColumn: "IdFerias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usufruto_IdFerias",
                table: "usufruto",
                column: "IdFerias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usufruto");

            migrationBuilder.DropTable(
                name: "ferias");
        }
    }
}
