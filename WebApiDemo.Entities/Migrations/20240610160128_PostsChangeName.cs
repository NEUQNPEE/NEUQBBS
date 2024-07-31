using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class PostsChangeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
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
                newName: "Posts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");
        }
    }
}
