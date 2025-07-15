using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Austistic.Core.Migrations
{
    /// <inheritdoc />
    public partial class removefriend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Friends");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Friends",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "text", nullable: false),
            //        FriendUserId = table.Column<string>(type: "text", nullable: true),
            //        UserId = table.Column<string>(type: "text", nullable: false),
            //        Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //        FriendId = table.Column<string>(type: "text", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            //        Status = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Friends", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Friends_AspNetUsers_FriendUserId",
            //            column: x => x.FriendUserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Friends_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Friends_FriendUserId",
            //    table: "Friends",
            //    column: "FriendUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Friends_UserId",
            //    table: "Friends",
            //    column: "UserId");
        }
    }
}
