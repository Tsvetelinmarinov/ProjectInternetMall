using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMall.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding_Data_To_Table_Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedOn", "CustomerName", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 11, 20, 9, 4, 338, DateTimeKind.Local).AddTicks(4249), "Angel Snopf", "Cancelled", 25.55m },
                    { 2, new DateTime(2026, 11, 11, 20, 9, 4, 341, DateTimeKind.Local).AddTicks(7180), "Tsvetelin Marinov", "Processing", 666m },
                    { 3, new DateTime(2026, 11, 11, 20, 9, 4, 341, DateTimeKind.Local).AddTicks(7236), "Martina Pavlova", "Shipped", 1555.65m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);
        }
    }
}
