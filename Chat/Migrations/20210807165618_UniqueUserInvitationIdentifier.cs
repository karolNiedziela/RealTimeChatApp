using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class UniqueUserInvitationIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvitationIdenfitier",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationIdenfitier",
                table: "AspNetUsers");
        }
    }
}
