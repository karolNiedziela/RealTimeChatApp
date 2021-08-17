using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class UpdateColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_RequestedUserId",
                table: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedUserId",
                table: "FriendRequests",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_RequestedUserId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "FriendRequests",
                newName: "RequestedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_RequestedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_RequestedUserId",
                table: "FriendRequests",
                column: "RequestedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
