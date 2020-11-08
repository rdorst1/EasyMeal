using Microsoft.EntityFrameworkCore.Migrations;

namespace MealManagement.Migrations
{
    public partial class Meal11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "Meals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ChefId",
                table: "Meals",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Chef_ChefId",
                table: "Meals",
                column: "ChefId",
                principalTable: "Chef",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Chef_ChefId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ChefId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Meals");
        }
    }
}
