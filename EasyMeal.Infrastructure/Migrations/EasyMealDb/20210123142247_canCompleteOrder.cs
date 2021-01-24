using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMeal.Infrastructure.Migrations.EasyMealDb
{
    public partial class canCompleteOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanCompleteOrder",
                table: "WeekOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanCompleteOrder",
                table: "WeekOrders");
        }
    }
}
