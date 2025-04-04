using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Austistic.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySymbol",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySymbol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySymbol_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SymbolImage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CategorySymbolId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    SymbolIdentifier = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymbolImage_CategorySymbol_CategorySymbolId",
                        column: x => x.CategorySymbolId,
                        principalTable: "CategorySymbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySymbol_UserId",
                table: "CategorySymbol",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolImage_CategorySymbolId",
                table: "SymbolImage",
                column: "CategorySymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymbolImage");

            migrationBuilder.DropTable(
                name: "CategorySymbol");
        }
    }
}
