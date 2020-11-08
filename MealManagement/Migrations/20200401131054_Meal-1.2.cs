using Microsoft.EntityFrameworkCore.Migrations;

namespace MealManagement.Migrations
{
    public partial class Meal12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Chef_ChefId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ChefId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "ChefId",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChefId",
                table: "Meals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
