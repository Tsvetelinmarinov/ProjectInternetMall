using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMall.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Order_Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 12, 19, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 12, 19, 51, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 12, 19, 51, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 11, 20, 9, 4, 338, DateTimeKind.Local).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 11, 20, 9, 4, 341, DateTimeKind.Local).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 11, 11, 20, 9, 4, 341, DateTimeKind.Local).AddTicks(7236));
        }
    }
}
