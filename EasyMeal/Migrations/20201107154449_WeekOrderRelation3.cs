using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMealOrder.Migrations
{
    public partial class WeekOrderRelation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId2",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WeekOrderId2",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WeekOrderId2",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekOrderId2",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WeekOrderId2",
                table: "Orders",
                column: "WeekOrderId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId2",
                table: "Orders",
                column: "WeekOrderId2",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
