using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Austistic.Core.Migrations
{
    /// <inheritdoc />
    public partial class updateroommessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_FriendId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "DisplayMessage",
                table: "RoomMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReadMassageCount",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MessageId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadMassageCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadMassageCount_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadMassageCount_RoomMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "RoomMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FriendId",
                table: "Rooms",
                column: "FriendId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadMassageCount_MessageId",
                table: "ReadMassageCount",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadMassageCount_UserId",
                table: "ReadMassageCount",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadMassageCount");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FriendId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DisplayMessage",
                table: "RoomMessages");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FriendId",
                table: "Rooms",
                column: "FriendId");
        }
    }
}
