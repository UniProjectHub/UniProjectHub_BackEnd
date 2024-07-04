using Microsoft.EntityFrameworkCore.Migrations;

namespace Infracstructures.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the index exists before attempting to drop it
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE Name = 'IX_Schedule_TeacherId'
                    AND Object_ID = Object_ID('Schedule')
                )
                BEGIN
                    DROP INDEX [IX_Schedule_TeacherId] ON [Schedule];
                END
            ");

            // Ensure the column 'TeacherId' is defined properly
            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Schedule",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Users_TeacherId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_TeacherId",
                table: "Schedule");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Schedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TeacherId1",
                table: "Schedule",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Users_TeacherId1",
                table: "Schedule",
                column: "TeacherId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
