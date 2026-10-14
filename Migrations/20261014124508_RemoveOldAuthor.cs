using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Porcilescu_Ionela_Delia_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
