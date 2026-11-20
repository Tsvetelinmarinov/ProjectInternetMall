using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace EMall.Data.SeedData;

/// <summary>
/// Seeds demo products when the product table is empty and ensures that
/// the demo orders are linked to those products.
/// </summary>
public static class AdminDashboardDataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        int[] requiredCategoryIds = [1, 2, 3, 4, 5, 6];

        var existingCategoryIds = dbContext
            .Categories
            .AsNoTracking()
            .Where(category => requiredCategoryIds.Contains(category.CategoryId))
            .Select(category => category.CategoryId)
            .ToList();

        if (existingCategoryIds.Count != requiredCategoryIds.Length)
        {
            throw new InvalidOperationException(
                "Първо трябва да бъдат създадени категориите с ID от 1 до 6.");
        }

        Product[] seededProducts =
        [
            new()
            {
                ProductName = "Lenovo IdeaPad Slim 5",
                Description = "Лек лаптоп за работа, учене и ежедневни задачи.",
                Price = 1499.99,
                CategoryId = 1,
                Claims = [
                    new() { ClaimName = "Памет", ClaimValue = "16 GB RAM" },
                    new() { ClaimName = "Диск", ClaimValue = "512 GB SSD" }
                ]
            },
            new()
            {
                ProductName = "ASUS TUF Gaming A15",
                Description = "Гейминг лаптоп с бърз дисплей и мощна видеокарта.",
                Price = 2499.99,
                CategoryId = 1,
                Claims = [
                    new() { ClaimName = "Памет", ClaimValue = "16 GB RAM" },
                    new() { ClaimName = "Диск", ClaimValue = "1 TB SSD" }
                ]
            },
            new()
            {
                ProductName = "Samsung Galaxy S25",
                Description = "Смартфон с ярък дисплей и качествена камера.",
                Price = 1799.99,
                CategoryId = 2,
                Claims = [
                    new() { ClaimName = "Памет", ClaimValue = "256 GB" },
                    new() { ClaimName = "Дисплей", ClaimValue = "AMOLED" }
                ]
            },
            new()
            {
                ProductName = "Xiaomi Redmi Note 14",
                Description = "Практичен смартфон с голяма батерия и ясен екран.",
                Price = 499.99,
                CategoryId = 2,
                Claims = [
                    new() { ClaimName = "Памет", ClaimValue = "128 GB" },
                    new() { ClaimName = "Батерия", ClaimValue = "5110 mAh" }
                ]
            },
            new()
            {
                ProductName = "Logitech G502 HERO",
                Description = "Гейминг мишка с прецизен сензор и програмируеми бутони.",
                Price = 119.99,
                CategoryId = 3,
                Claims = [
                    new() { ClaimName = "Свързване", ClaimValue = "USB" },
                    new() { ClaimName = "Бутони", ClaimValue = "11 програмируеми" }
                ]
            },
            new()
            {
                ProductName = "HyperX Alloy Origins",
                Description = "Механична клавиатура с RGB подсветка за игри и работа.",
                Price = 219.99,
                CategoryId = 3,
                Claims = [
                    new() { ClaimName = "Свързване", ClaimValue = "USB-C" },
                    new() { ClaimName = "Подсветка", ClaimValue = "RGB" }
                ]
            },
            new()
            {
                ProductName = "Sony WH-1000XM5",
                Description = "Безжични слушалки с активно шумопотискане и чист звук.",
                Price = 699.99,
                CategoryId = 4,
                Claims = [
                    new() { ClaimName = "Свързване", ClaimValue = "Bluetooth" },
                    new() { ClaimName = "Шумопотискане", ClaimValue = "Активно" }
                ]
            },
            new()
            {
                ProductName = "JBL Flip 6",
                Description = "Преносима Bluetooth тонколона с водоустойчив корпус.",
                Price = 229.99,
                CategoryId = 4,
                Claims = [
                    new() { ClaimName = "Свързване", ClaimValue = "Bluetooth" },
                    new() { ClaimName = "Защита", ClaimValue = "IP67" }
                ]
            },
            new()
            {
                ProductName = "TP-Link Tapo C220",
                Description = "Вътрешна камера за наблюдение с въртене и нощен режим.",
                Price = 89.99,
                CategoryId = 5,
                Claims = [
                    new() { ClaimName = "Резолюция", ClaimValue = "2K" },
                    new() { ClaimName = "Свързване", ClaimValue = "Wi-Fi" }
                ]
            },
            new()
            {
                ProductName = "Philips Hue White",
                Description = "Умна LED крушка, която се управлява през мобилно приложение.",
                Price = 39.99,
                CategoryId = 5,
                Claims = [
                    new() { ClaimName = "Цокъл", ClaimValue = "E27" },
                    new() { ClaimName = "Свързване", ClaimValue = "Bluetooth" }
                ]
            },
            new()
            {
                ProductName = "Anker Nano Charger",
                Description = "Компактно USB-C зарядно за телефон и други устройства.",
                Price = 49.99,
                CategoryId = 6,
                Claims = [
                    new() { ClaimName = "Мощност", ClaimValue = "30 W" },
                    new() { ClaimName = "Порт", ClaimValue = "USB-C" }
                ]
            },
            new()
            {
                ProductName = "Ugreen USB-C Cable",
                Description = "Издръжлив USB-C кабел за зареждане и пренос на данни.",
                Price = 19.99,
                CategoryId = 6,
                Claims = [
                    new() { ClaimName = "Дължина", ClaimValue = "1 m" },
                    new() { ClaimName = "Конектор", ClaimValue = "USB-C" }
                ]
            }
        ];

        string[] seededProductNames = seededProducts
            .Select(product => product.ProductName)
            .ToArray();

        if (await dbContext.Products.AnyAsync())
        {
            var existingSeededProducts = await dbContext.Products
                .Where(product => seededProductNames.Contains(product.ProductName))
                .ToListAsync();

            if (existingSeededProducts.Count != seededProducts.Length)
            {
                return;
            }

            seededProducts = existingSeededProducts
                .OrderBy(product => Array.IndexOf(seededProductNames, product.ProductName))
                .ToArray();
        }
        else
        {
            await dbContext.Products.AddRangeAsync(seededProducts);
            await dbContext.SaveChangesAsync();
        }

        int[] seededOrderIds = [1, 2, 3];
        const int ProductsPerSeededOrder = 4;

        if (seededProducts.Length != seededOrderIds.Length * ProductsPerSeededOrder)
        {
            throw new InvalidOperationException("Seed products must be grouped evenly across the demo orders.");
        }

        var existingOrderIds = await dbContext.Orders
            .Where(order => seededOrderIds.Contains(order.OrderId))
            .Select(order => order.OrderId)
            .ToListAsync();

        if (existingOrderIds.Count != seededOrderIds.Length)
        {
            throw new InvalidOperationException(
                "Първо трябва да бъдат създадени seed поръчките с ID от 1 до 3.");
        }

        var expectedOrderProducts = seededProducts
            .Select((product, index) => new OrderProduct
            {
                OrderId = seededOrderIds[index / ProductsPerSeededOrder],
                ProductId = product.ProductId
            })
            .ToArray();

        var existingOrderProducts = await dbContext.OrderProducts
            .Where(orderProduct => seededOrderIds.Contains(orderProduct.OrderId))
            .Select(orderProduct => new { orderProduct.OrderId, orderProduct.ProductId })
            .ToListAsync();

        var existingOrderProductKeys = existingOrderProducts
            .Select(orderProduct => (orderProduct.OrderId, orderProduct.ProductId))
            .ToHashSet();

        var missingOrderProducts = expectedOrderProducts
            .Where(orderProduct => !existingOrderProductKeys.Contains((orderProduct.OrderId, orderProduct.ProductId)))
            .ToArray();

        if (missingOrderProducts.Length > 0)
        {
            await dbContext.OrderProducts.AddRangeAsync(missingOrderProducts);
            await dbContext.SaveChangesAsync();
        }
    }
}