using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeb4UserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Web4User",
                table: "Web4User");

            migrationBuilder.RenameTable(
                name: "Web4User",
                newName: "Web4Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Web4Users",
                table: "Web4Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Web4Users",
                table: "Web4Users");

            migrationBuilder.RenameTable(
                name: "Web4Users",
                newName: "Web4User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Web4User",
                table: "Web4User",
                column: "Id");
        }
    }
}
