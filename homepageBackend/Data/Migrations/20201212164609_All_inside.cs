using Microsoft.EntityFrameworkCore.Migrations;

namespace homepageBackend.Data.Migrations
{
    public partial class All_inside : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_CreatorId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_UpdaterId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_CreatorId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_UpdaterId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_UserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatorId",
                table: "Documents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UpdaterId",
                table: "Documents",
                column: "UpdaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_CreatorId",
                table: "Documents",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_UpdaterId",
                table: "Documents",
                column: "UpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
