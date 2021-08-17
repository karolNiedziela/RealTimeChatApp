using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class ChangeRoomInvitationTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomInvitation_AspNetUsers_UserId",
                table: "RoomInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomInvitation_Rooms_RoomId",
                table: "RoomInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomInvitation",
                table: "RoomInvitation");

            migrationBuilder.RenameTable(
                name: "RoomInvitation",
                newName: "RoomInvitations");

            migrationBuilder.RenameIndex(
                name: "IX_RoomInvitation_UserId",
                table: "RoomInvitations",
                newName: "IX_RoomInvitations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomInvitation_RoomId",
                table: "RoomInvitations",
                newName: "IX_RoomInvitations_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomInvitations",
                table: "RoomInvitations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomInvitations_AspNetUsers_UserId",
                table: "RoomInvitations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomInvitations_Rooms_RoomId",
                table: "RoomInvitations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomInvitations_AspNetUsers_UserId",
                table: "RoomInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomInvitations_Rooms_RoomId",
                table: "RoomInvitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomInvitations",
                table: "RoomInvitations");

            migrationBuilder.RenameTable(
                name: "RoomInvitations",
                newName: "RoomInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_RoomInvitations_UserId",
                table: "RoomInvitation",
                newName: "IX_RoomInvitation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomInvitations_RoomId",
                table: "RoomInvitation",
                newName: "IX_RoomInvitation_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomInvitation",
                table: "RoomInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomInvitation_AspNetUsers_UserId",
                table: "RoomInvitation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomInvitation_Rooms_RoomId",
                table: "RoomInvitation",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
