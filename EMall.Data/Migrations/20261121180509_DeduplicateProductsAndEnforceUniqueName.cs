using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMall.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeduplicateProductsAndEnforceUniqueName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE TABLE #DuplicateProducts
                (
                    ProductId int NOT NULL PRIMARY KEY,
                    CanonicalProductId int NOT NULL
                );

                ;WITH RankedProducts AS
                (
                    SELECT ProductId,
                           FIRST_VALUE(ProductId) OVER
                           (
                               PARTITION BY ProductName
                               ORDER BY CASE WHEN IsDeleted = 0 THEN 0 ELSE 1 END, ProductId
                           ) AS CanonicalProductId,
                           ROW_NUMBER() OVER
                           (
                               PARTITION BY ProductName
                               ORDER BY CASE WHEN IsDeleted = 0 THEN 0 ELSE 1 END, ProductId
                           ) AS RowNumber
                    FROM Products
                )
                INSERT INTO #DuplicateProducts (ProductId, CanonicalProductId)
                SELECT ProductId, CanonicalProductId
                FROM RankedProducts
                WHERE RowNumber > 1;

                INSERT INTO OrderProducts (OrderId, ProductId)
                SELECT DISTINCT orderProducts.OrderId, duplicates.CanonicalProductId
                FROM OrderProducts AS orderProducts
                INNER JOIN #DuplicateProducts AS duplicates
                    ON orderProducts.ProductId = duplicates.ProductId
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM OrderProducts AS existingOrderProducts
                    WHERE existingOrderProducts.OrderId = orderProducts.OrderId
                      AND existingOrderProducts.ProductId = duplicates.CanonicalProductId
                );

                DELETE orderProducts
                FROM OrderProducts AS orderProducts
                INNER JOIN #DuplicateProducts AS duplicates
                    ON orderProducts.ProductId = duplicates.ProductId;

                DELETE products
                FROM Products AS products
                INNER JOIN #DuplicateProducts AS duplicates
                    ON products.ProductId = duplicates.ProductId;

                DROP TABLE #DuplicateProducts;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductName",
                table: "Products");
        }
    }
}
