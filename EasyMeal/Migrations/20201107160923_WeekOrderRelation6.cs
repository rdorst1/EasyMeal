using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMealOrder.Migrations
{
    public partial class WeekOrderRelation6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WeekOrderId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders",
                column: "WeekOrderId",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WeekOrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders",
                column: "WeekOrderId",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
