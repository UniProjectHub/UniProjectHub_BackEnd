using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infracstructures.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "243b9fb3-c86b-42eb-a851-98dcb874d3ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d18e80-5469-4ee8-a9a4-34cb87204dcc");

            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0410269c-7003-47fb-bf40-9a5e93922efc", null, "Admin", "ADMIN" },
                    { "a3028f44-5968-4b77-a58d-62d0817ce9ff", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0410269c-7003-47fb-bf40-9a5e93922efc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3028f44-5968-4b77-a58d-62d0817ce9ff");

            migrationBuilder.DropColumn(
                name: "IsTeacher",
                table: "Users");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "243b9fb3-c86b-42eb-a851-98dcb874d3ff", null, "Admin", "ADMIN" },
                    { "87d18e80-5469-4ee8-a9a4-34cb87204dcc", null, "User", "USER" }
                });
        }
    }
}
