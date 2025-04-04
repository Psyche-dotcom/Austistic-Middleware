using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Austistic.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatecategoryLATEST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryType",
                table: "CategorySymbol",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "CategorySymbol");
        }
    }
}
