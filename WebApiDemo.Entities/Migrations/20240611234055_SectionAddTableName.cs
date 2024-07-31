using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class SectionAddTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "Sections",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "Sections");
        }
    }
}
