using Microsoft.EntityFrameworkCore.Migrations;

namespace homepageBackend.Data.Migrations
{
    public partial class Added_DocumentTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_Documents_DocumentId",
                table: "DocumentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_Tags_TagName",
                table: "DocumentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTag_Projects_ProjectId",
                table: "ProjectTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTag_Tags_TagName",
                table: "ProjectTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTag",
                table: "ProjectTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag");

            migrationBuilder.RenameTable(
                name: "ProjectTag",
                newName: "ProjectTags");

            migrationBuilder.RenameTable(
                name: "DocumentTag",
                newName: "DocumentTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTag_TagName",
                table: "ProjectTags",
                newName: "IX_ProjectTags_TagName");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTag_TagName",
                table: "DocumentTags",
                newName: "IX_DocumentTags_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags",
                columns: new[] { "ProjectId", "TagName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTags",
                table: "DocumentTags",
                columns: new[] { "DocumentId", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTags_Documents_DocumentId",
                table: "DocumentTags",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTags_Tags_TagName",
                table: "DocumentTags",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_DocumentTags_Documents_DocumentId",
                table: "DocumentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTags_Tags_TagName",
                table: "DocumentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Projects_ProjectId",
                table: "ProjectTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Tags_TagName",
                table: "ProjectTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTags",
                table: "DocumentTags");

            migrationBuilder.RenameTable(
                name: "ProjectTags",
                newName: "ProjectTag");

            migrationBuilder.RenameTable(
                name: "DocumentTags",
                newName: "DocumentTag");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTags_TagName",
                table: "ProjectTag",
                newName: "IX_ProjectTag_TagName");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTags_TagName",
                table: "DocumentTag",
                newName: "IX_DocumentTag_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTag",
                table: "ProjectTag",
                columns: new[] { "ProjectId", "TagName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTag",
                table: "DocumentTag",
                columns: new[] { "DocumentId", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_Documents_DocumentId",
                table: "DocumentTag",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_Tags_TagName",
                table: "DocumentTag",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTag_Projects_ProjectId",
                table: "ProjectTag",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTag_Tags_TagName",
                table: "ProjectTag",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
