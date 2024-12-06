using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeriasTJBase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColunmName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Ferias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Ferias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
