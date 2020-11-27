using Microsoft.EntityFrameworkCore.Migrations;

namespace homepageBackend.Data.Migrations
{
    public partial class Renamed_posttags_to_projecttags2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Projects_ProjectId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagName",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "ProjectTags");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagName",
                table: "ProjectTags",
                newName: "IX_ProjectTags_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags",
                columns: new[] { "ProjectId", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTags_Projects_ProjectId",
                table: "ProjectTags",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTags_Tags_TagName",
                table: "ProjectTags",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Projects_ProjectId",
                table: "ProjectTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Tags_TagName",
                table: "ProjectTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags");

            migrationBuilder.RenameTable(
                name: "ProjectTags",
                newName: "PostTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTags_TagName",
                table: "PostTags",
                newName: "IX_PostTags_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                columns: new[] { "ProjectId", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Projects_ProjectId",
                table: "PostTags",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagName",
                table: "PostTags",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
