using Microsoft.EntityFrameworkCore.Migrations;

namespace MealManagement.Migrations
{
    public partial class DayOfTheWeekAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOfTheWeek",
                table: "Meals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "Meals");
        }
    }
}
