using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FeriasTJBase.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableFeriasUsufruto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ferias",
                columns: table => new
                {
                    IdFerias = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matricula = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    PeriodoAquisitivoInicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodoAquisitivoFinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias", x => x.IdFerias);
                });

            migrationBuilder.CreateTable(
                name: "Usufruto",
                columns: table => new
                {
                    IdUsufruto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdFerias = table.Column<int>(type: "integer", nullable: false),
                    UsufrutoInicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsufrutoFinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usufruto", x => x.IdUsufruto);
                    table.ForeignKey(
                        name: "FK_Usufruto_Ferias_IdFerias",
                        column: x => x.IdFerias,
                        principalTable: "Ferias",
                        principalColumn: "IdFerias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usufruto_IdFerias",
                table: "Usufruto",
                column: "IdFerias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usufruto");

            migrationBuilder.DropTable(
                name: "Ferias");
        }
    }
}
