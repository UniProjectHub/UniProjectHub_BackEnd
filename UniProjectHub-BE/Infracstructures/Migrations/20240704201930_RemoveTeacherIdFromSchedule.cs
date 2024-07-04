using Microsoft.EntityFrameworkCore.Migrations;

namespace Infracstructures.Migrations
{
    public partial class RemoveTeacherIdFromSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Users_TeacherId",
                table: "Schedule");

           

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Schedule");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Schedule",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TeacherId",
                table: "Schedule",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Users_TeacherId",
                table: "Schedule",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Modify ON DELETE action as per your requirement
        }
    }
}
