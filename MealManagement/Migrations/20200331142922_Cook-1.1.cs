using Microsoft.EntityFrameworkCore.Migrations;

namespace MealManagement.Migrations
{
    public partial class Cook11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chef");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Chef",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Chef",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Chef");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Chef");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chef",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
