using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMealOrder.Migrations
{
    public partial class WeekOrderRelation5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WeekOrderId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WeekOrderId1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WeekOrderId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders",
                column: "WeekOrderId",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WeekOrderId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WeekOrderId1",
                table: "Orders",
                column: "WeekOrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId",
                table: "Orders",
                column: "WeekOrderId",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeekOrders_WeekOrderId1",
                table: "Orders",
                column: "WeekOrderId1",
                principalTable: "WeekOrders",
                principalColumn: "WeekOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
