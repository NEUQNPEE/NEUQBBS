using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "ComprehensiveSectionPosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComprehensiveSectionPosts",
                table: "ComprehensiveSectionPosts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComprehensiveSectionPosts",
                table: "ComprehensiveSectionPosts");

            migrationBuilder.RenameTable(
                name: "ComprehensiveSectionPosts",
                newName: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");
        }
    }
}
