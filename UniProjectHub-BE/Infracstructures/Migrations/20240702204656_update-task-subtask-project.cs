using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infracstructures.Migrations
{
    /// <inheritdoc />
    public partial class updatetasksubtaskproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Task",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "SubTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Project");

           
        }
    }
}
