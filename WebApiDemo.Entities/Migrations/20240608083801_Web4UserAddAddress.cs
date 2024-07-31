using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Web4UserAddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Web4Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Web4Users");
        }
    }
}
