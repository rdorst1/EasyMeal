using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMealOrder.Migrations
{
    public partial class WeekOrderCompletedBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "WeekOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "WeekOrders");
        }
    }
}
