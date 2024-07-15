using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coderama.DocumentManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddValueColumnToTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Tags");
        }
    }
}
