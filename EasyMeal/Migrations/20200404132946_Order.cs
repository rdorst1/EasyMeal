using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMealOrder.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "Appetizer",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Dessert",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderSize",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "WeekOrderId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeekOrders",
                columns: table => new
                {
                    WeekOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerUserId = table.Column<int>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekOrders", x => x.WeekOrderId);
                    table.ForeignKey(
                        name: "FK_WeekOrders_Users_CustomerUserId",
                        column: x => x.CustomerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WeekOrderId",
                table: "Orders",
                column: "WeekOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekOrders_CustomerUserId",
                table: "WeekOrders",
                column: "CustomerUserId");

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

            migrationBuilder.DropTable(
                name: "WeekOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WeekOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Appetizer",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Dessert",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderSize",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WeekOrderId",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
