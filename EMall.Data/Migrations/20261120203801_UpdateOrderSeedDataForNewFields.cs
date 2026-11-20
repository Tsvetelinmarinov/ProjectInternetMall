using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMall.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderSeedDataForNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressForShipping",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "AddressForShipping", "CustomerEmail" },
                values: new object[] { "бул. Витоша 100, София", "angel.snopf@example.com" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "AddressForShipping", "CustomerEmail" },
                values: new object[] { "ул. Иван Вазов 25, Пловдив", "tsvetelin.marinov@example.com" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                columns: new[] { "AddressForShipping", "CustomerEmail" },
                values: new object[] { "ул. Александровска 40, Бургас", "martina.pavlova@example.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressForShipping",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Orders");
        }
    }
}
