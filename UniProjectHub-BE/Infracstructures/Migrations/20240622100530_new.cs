using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infracstructures.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "429cac3d-7594-4524-b6b4-32876dd1ab13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8a1e9f1-823c-4fed-b304-bce22156d777");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3647bf7d-9652-4d27-a217-0c884f7efa9a", null, "User", "USER" },
                    { "5a3f358e-f83e-4d52-b485-a51d6c490eb1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3647bf7d-9652-4d27-a217-0c884f7efa9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a3f358e-f83e-4d52-b485-a51d6c490eb1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "429cac3d-7594-4524-b6b4-32876dd1ab13", null, "User", "USER" },
                    { "d8a1e9f1-823c-4fed-b304-bce22156d777", null, "Admin", "ADMIN" }
                });
        }
    }
}
