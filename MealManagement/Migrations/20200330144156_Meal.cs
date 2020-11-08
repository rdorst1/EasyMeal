using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealManagement.Migrations
{
    public partial class Meal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    Appetizer = table.Column<string>(nullable: true),
                    MainDish = table.Column<string>(nullable: true),
                    Dessert = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Saltless = table.Column<bool>(nullable: false),
                    Diabetes = table.Column<bool>(nullable: false),
                    GlutenAllergy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
