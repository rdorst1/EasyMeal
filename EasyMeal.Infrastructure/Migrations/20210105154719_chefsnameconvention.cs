using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMeal.Infrastructure.Migrations
{
    public partial class chefsnameconvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    ChefId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.ChefId);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    Appetizer = table.Column<string>(nullable: true),
                    MainDish = table.Column<string>(nullable: true),
                    Dessert = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DayOfTheWeek = table.Column<string>(nullable: true),
                    Saltless = table.Column<bool>(nullable: false),
                    Diabetes = table.Column<bool>(nullable: false),
                    GlutenAllergy = table.Column<bool>(nullable: false),
                    ChefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
