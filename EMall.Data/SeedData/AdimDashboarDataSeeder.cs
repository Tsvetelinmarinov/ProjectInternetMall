using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;

namespace EMall.Data.SeedData;

/// <summary>
///  This static class and his static asynchronous method
///  SeedAsync() seeds data(products) for the admin dashboard, if not
///  any products and orders.
/// </summary>
public static class AdminDashboardDataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        // If any products -> use them, do not seed nothing.
        if (dbContext.Products.Any())
        {
            return;
        }

        int[] requiredCategoryIds = [1, 2, 3, 4, 5, 6];

        var existingCategoryIds = dbContext
            .Categories
            .Where(category => requiredCategoryIds.Contains(category.CategoryId))
            .Select(category => category.CategoryId)
            .ToList();

        if (existingCategoryIds.Count != requiredCategoryIds.Length)
        {
            throw new InvalidOperationException(
                "Първо трябва да бъдат създадени категориите с ID от 1 до 6.");
        }

        Product[] products =
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

        await dbContext.Products.AddRangeAsync(products);
        await dbContext.SaveChangesAsync();
    }
}