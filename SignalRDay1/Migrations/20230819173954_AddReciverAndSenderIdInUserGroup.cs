using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRDay1.Migrations
{
    /// <inheritdoc />
    public partial class AddReciverAndSenderIdInUserGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserGroups",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups",
                newName: "IX_UserGroups_SenderId");

            migrationBuilder.AddColumn<string>(
                name: "RecivedId",
                table: "UserGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_RecivedId",
                table: "UserGroups",
                column: "RecivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_RecivedId",
                table: "UserGroups",
                column: "RecivedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_SenderId",
                table: "UserGroups",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_RecivedId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_SenderId",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_RecivedId",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "RecivedId",
                table: "UserGroups");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "UserGroups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroups_SenderId",
                table: "UserGroups",
                newName: "IX_UserGroups_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
