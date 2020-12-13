using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace homepageBackend.Data.Migrations
{
    public partial class Changed_Tag_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Tags",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdaterId",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdaterId",
                table: "Projects",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
