using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infracstructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsMemberCardUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<bool>(
                name: "IsMemberCard",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

           

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "IsMemberCard",
                table: "Users");

           

           
        }
    }
}
