using EMall.Data.DatabaseContext;
using EMall.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using static EMall.Data.EntityModels.Common.DbConstants.AdminDashboardDataSeederRelated;

namespace EMall.Data.SeedData;

/// <summary>
/// Seeds demo products when the product table is empty and ensures that
/// the demo orders are linked to those products.
/// </summary>
public static class AdminDashboardDataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        // Maded by Github Copilot to enable unique names on products.
        await dbContext.Database.ExecuteSqlRawAsync(
            """
                DECLARE @lockResult int;
                EXEC @lockResult = sp_getapplock
                    @Resource = N'EMall.AdminDashboardDataSeeder',
                    @LockMode = 'Exclusive',
                    @LockOwner = 'Transaction',
                    @LockTimeout = 30000;

                IF @lockResult < 0
                    THROW 51000, 'Could not acquire the dashboard seeder lock.', 1;
            """
        );

        // Needed categories IDs.
        int[] requiredCategoryIds = [1, 2, 3, 4, 5, 6];

        // Take the categories with the needed IDs from the database.
        var existingCategoryIds = dbContext
            .Categories
            .AsNoTracking()
            .Where(category => requiredCategoryIds.Contains(category.CategoryId))
            .Select(category => category.CategoryId)
            .ToList();

        // If the count of existing categories is not equals to the count of
        // categories that we require - that's mean some(or all)
        // category does not exist yet.
        if (existingCategoryIds.Count != requiredCategoryIds.Length)
        {
            throw new InvalidOperationException(CategoriesDoesNotExist);
        }

        // Mock product for first database initialization.
        // That products will be seeded first, and will left in the database.
        Product[] seedProducts =
        [
            new()
            {
                ProductName = "Lenovo IdeaPad Slim 5",
                Description = "Лек лаптоп за работа, учене и ежедневни задачи.",
                Price = 1499.99,
                CategoryId = 1,
                Claims = [
                    new() { ClaimName = "Памет", ClaimValue = "16 GB RAM" },
                    new() { ClaimName = "Диск", ClaimValue = "512 GB SSD" },
                    new() { ClaimName = "Процесор", ClaimValue = "AMD Ryzen 5 7530U" },
                    new() { ClaimName = "Екран", ClaimValue = "14 инча WUXGA IPS" },
                    new() { ClaimName = "Графика", ClaimValue = "AMD Radeon Graphics" },
                    new() { ClaimName = "Операционна система", ClaimValue = "Windows 11 Home" },
                    new() { ClaimName = "Камера", ClaimValue = "Full HD с капаче за поверителност" },
                    new() { ClaimName = "Безжична връзка", ClaimValue = "Wi-Fi 6 и Bluetooth 5.2" }
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
                    new() { ClaimName = "Диск", ClaimValue = "1 TB SSD" },
                    new() { ClaimName = "Процесор", ClaimValue = "AMD Ryzen 7 7735HS" },
                    new() { ClaimName = "Видеокарта", ClaimValue = "NVIDIA GeForce RTX 4060 8 GB" },
                    new() { ClaimName = "Екран", ClaimValue = "15,6 инча Full HD, 144 Hz" },
                    new() { ClaimName = "Операционна система", ClaimValue = "Windows 11 Home" },
                    new() { ClaimName = "Безжична връзка", ClaimValue = "Wi-Fi 6 и Bluetooth 5.2" },
                    new() { ClaimName = "Клавиатура", ClaimValue = "Подсветка с RGB осветление" }
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
                    new() { ClaimName = "Дисплей", ClaimValue = "6,2 инча Dynamic AMOLED 2X, 120 Hz" },
                    new() { ClaimName = "Процесор", ClaimValue = "Snapdragon 8 Elite for Galaxy" },
                    new() { ClaimName = "Основна камера", ClaimValue = "50 MP с оптична стабилизация" },
                    new() { ClaimName = "Предна камера", ClaimValue = "12 MP" },
                    new() { ClaimName = "Операционна система", ClaimValue = "Android 15 с One UI 7" },
                    new() { ClaimName = "Защита", ClaimValue = "IP68 срещу прах и вода" },
                    new() { ClaimName = "Свързване", ClaimValue = "5G, NFC и USB-C" }
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
                    new() { ClaimName = "Батерия", ClaimValue = "5110 mAh" },
                    new() { ClaimName = "Дисплей", ClaimValue = "6,67 инча AMOLED, 120 Hz" },
                    new() { ClaimName = "Процесор", ClaimValue = "MediaTek Helio G99-Ultra" },
                    new() { ClaimName = "Основна камера", ClaimValue = "108 MP" },
                    new() { ClaimName = "Зареждане", ClaimValue = "Бързо зареждане до 33 W" },
                    new() { ClaimName = "Операционна система", ClaimValue = "Android с Xiaomi HyperOS" },
                    new() { ClaimName = "Защита", ClaimValue = "IP54 срещу прах и пръски" }
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
                    new() { ClaimName = "Бутони", ClaimValue = "11 програмируеми" },
                    new() { ClaimName = "Сензор", ClaimValue = "Оптичен HERO 25K" },
                    new() { ClaimName = "Чувствителност", ClaimValue = "До 25 600 DPI" },
                    new() { ClaimName = "Подсветка", ClaimValue = "LIGHTSYNC RGB" },
                    new() { ClaimName = "Тегло", ClaimValue = "121 g без допълнителни тежести" },
                    new() { ClaimName = "Кабел", ClaimValue = "USB кабел с дължина 2,1 m" },
                    new() { ClaimName = "Профили", ClaimValue = "Вградена памет за профили" }
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
                    new() { ClaimName = "Подсветка", ClaimValue = "RGB с индивидуално осветяване на клавишите" },
                    new() { ClaimName = "Корпус", ClaimValue = "Алуминиева горна плоча" },
                    new() { ClaimName = "Превключватели", ClaimValue = "Механични HyperX" },
                    new() { ClaimName = "Anti-ghosting", ClaimValue = "Пълно разпознаване на едновременни натискания" },
                    new() { ClaimName = "N-key rollover", ClaimValue = "Поддържа едновременно регистриране на клавишите" },
                    new() { ClaimName = "Съвместимост", ClaimValue = "Windows и macOS" },
                    new() { ClaimName = "Профили", ClaimValue = "Вградена памет за профили и макроси" }
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
                    new() { ClaimName = "Шумопотискане", ClaimValue = "Активно с адаптивно управление" },
                    new() { ClaimName = "Батерия", ClaimValue = "До 30 часа с включено шумопотискане" },
                    new() { ClaimName = "Бързо зареждане", ClaimValue = "3 минути зареждане за до 3 часа слушане" },
                    new() { ClaimName = "Микрофони", ClaimValue = "Вградени микрофони за разговори и ANC" },
                    new() { ClaimName = "Многоточково свързване", ClaimValue = "Едновременна връзка с две устройства" },
                    new() { ClaimName = "Тегло", ClaimValue = "Около 250 g" },
                    new() { ClaimName = "Зареждане", ClaimValue = "USB-C" }
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
                    new() { ClaimName = "Защита", ClaimValue = "IP67 срещу прах и вода" },
                    new() { ClaimName = "Мощност", ClaimValue = "30 W RMS" },
                    new() { ClaimName = "Батерия", ClaimValue = "До 12 часа възпроизвеждане" },
                    new() { ClaimName = "Зареждане", ClaimValue = "USB-C" },
                    new() { ClaimName = "Звукова система", ClaimValue = "Двупосочни високоговорители" },
                    new() { ClaimName = "PartyBoost", ClaimValue = "Свързване с други съвместими JBL тонколони" },
                    new() { ClaimName = "Микрофон", ClaimValue = "Вграден за разговори със свободни ръце" }
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
                    new() { ClaimName = "Свързване", ClaimValue = "Wi-Fi 2,4 GHz" },
                    new() { ClaimName = "Зрителен ъгъл", ClaimValue = "Панорамиране 360° и накланяне 114°" },
                    new() { ClaimName = "Нощен режим", ClaimValue = "Инфрачервено нощно виждане" },
                    new() { ClaimName = "Аудио", ClaimValue = "Двупосочно аудио с вграден микрофон и говорител" },
                    new() { ClaimName = "Проследяване", ClaimValue = "Откриване и проследяване на движение" },
                    new() { ClaimName = "Съхранение", ClaimValue = "MicroSD карта до 512 GB" },
                    new() { ClaimName = "Управление", ClaimValue = "Мобилно приложение Tapo" }
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
                    new() { ClaimName = "Свързване", ClaimValue = "Bluetooth и Zigbee" },
                    new() { ClaimName = "Светлинен поток", ClaimValue = "806 lm" },
                    new() { ClaimName = "Цветна температура", ClaimValue = "Топла бяла светлина, 2700 K" },
                    new() { ClaimName = "Мощност", ClaimValue = "9,5 W" },
                    new() { ClaimName = "Димиране", ClaimValue = "Регулиране на яркостта през приложение" },
                    new() { ClaimName = "Живот", ClaimValue = "До 25 000 часа" },
                    new() { ClaimName = "Управление", ClaimValue = "Philips Hue и съвместими приложения" }
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
                    new() { ClaimName = "Порт", ClaimValue = "USB-C" },
                    new() { ClaimName = "Технология", ClaimValue = "GaN за компактно и ефективно зареждане" },
                    new() { ClaimName = "Стандарт", ClaimValue = "USB Power Delivery" },
                    new() { ClaimName = "Съвместимост", ClaimValue = "Телефони, таблети и USB-C аксесоари" },
                    new() { ClaimName = "Защита", ClaimValue = "Защита от прегряване и претоварване" },
                    new() { ClaimName = "Дизайн", ClaimValue = "Компактен корпус за пътуване" },
                    new() { ClaimName = "Захранване", ClaimValue = "100–240 V AC" }
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
                    new() { ClaimName = "Конектор", ClaimValue = "USB-C към USB-C" },
                    new() { ClaimName = "Зареждане", ClaimValue = "Поддържа до 60 W Power Delivery" },
                    new() { ClaimName = "Пренос на данни", ClaimValue = "USB 2.0, до 480 Mbps" },
                    new() { ClaimName = "Ток", ClaimValue = "До 3 A" },
                    new() { ClaimName = "Оплетка", ClaimValue = "Усилена оплетка за ежедневна употреба" },
                    new() { ClaimName = "Съвместимост", ClaimValue = "Телефони, таблети и лаптопи с USB-C" },
                    new() { ClaimName = "Цвят", ClaimValue = "Черен" }
                ]
            }
        ];

        // Get the names of the products in an array.
        string[] seededProductNames = [.. seedProducts.Select(product => product.ProductName)];

        // Check if there are products in the database?
        // If the database already has products
        if (await dbContext.Products.AnyAsync())
        {
            // If we has products in the database - take that one with the same names
            // as the seeded one.
            var existingSeededProducts = await dbContext
                .Products
                .Where(product => seededProductNames.Contains(product.ProductName))
                .ToListAsync();

            // If the count of the existing seeded products is not equalst to the
            // count of seedProducts array with mock data, thats mean on startup
            // something went wrong and not all the mock data has been seeded.
            if (existingSeededProducts.Count != seedProducts.Length)
            {
                return;
            }

            // Convert the array with mock data(seedProducts) to a dictionary.
            // Each product will be key-value pair - product name and product object.
            var seedProductsDictionary = seedProducts.ToDictionary(product => product.ProductName);

            // Set each seeded products claims to the new ones after the update, because i add
            // more claims to each product. First i seed the products with 2 claims for each and
            // after that i add more 6 to each. So if we seed the first products with the original
            // 2 claims, the new claims will not be added, so we need to check and to add them
            // in this case.
            foreach (var existingSeededProduct in existingSeededProducts)
            {
                existingSeededProduct.Claims = seedProductsDictionary[existingSeededProduct.ProductName].Claims;
            }

            // Update database after the new claism INSERT operation.
            await dbContext.SaveChangesAsync();

            // Update the value of the mock data Product array(seedProcucts),
            // to reflect the new changes, if we make any at all.
            seedProducts = [.. 
                existingSeededProducts
                    .OrderBy(product => Array.IndexOf(seededProductNames, product.ProductName))
            ];
        }
        else // If the database has not products(is on startup) - seed our seedProducts here.
        {
            await dbContext.Products.AddRangeAsync(seedProducts);
            await dbContext.SaveChangesAsync();
        }

        int[] seedOrderIds = [1, 2, 3]; //=> We need orders with IDs 1, 2 and 3.

        // 4 product in one order in out test case:
        // 3 order with 4 products per order.
        const int ProductsPerSeededOrder = 4;

        // If we have less or more than  12 products, throw an error.
        // They need to be exatly 12 - 3 orders with 4 products per order.
        if (seedProducts.Length != seedOrderIds.Length * ProductsPerSeededOrder)
        {
            throw new InvalidOperationException(InvalidSeedProductsGrouping);
        }

        // Retrieve the orders with IDs 1, 2 and 3 from the database.
        var existingOrderIds = await dbContext
            .Orders
            .Where(order => seedOrderIds.Contains(order.OrderId))
            .Select(order => order.OrderId)
            .ToListAsync();

        // If orders with IDs 1, 2 or 3 does not exist, we 
        // should create them first.
        if (existingOrderIds.Count != seedOrderIds.Length)
        {
            throw new InvalidOperationException(NoMockOrdersAvailableInDb);
        }

        // Create the products for each order(OrderProduct) from the seeded products.
        var expectedOrderProducts = seedProducts
            .Select((product, index) => new OrderProduct
            {
                OrderId = seedOrderIds[index / ProductsPerSeededOrder],
                ProductId = product.ProductId
            })
            .ToArray();

        // Get a list with anonymous objects(the order products) from the database.
        // Extract the products for orders with IDs 1, 2 and 3. They shoul be
        // total 12 order products - each order has 4 products.
        var existingOrderProducts = await dbContext
            .OrderProducts
            .Where(orderProduct => seedOrderIds.Contains(orderProduct.OrderId))
            .Select(orderProduct => new { orderProduct.OrderId, orderProduct.ProductId })
            .ToListAsync();

        // Make a HashSet with tupes of order IDs and product IDs.
        var existingOrderProductKeys = existingOrderProducts
            .Select(orderProduct => (orderProduct.OrderId, orderProduct.ProductId))
            .ToHashSet();

        // If any order missing some of her 4 seed products for some reason,
        // seed them again.
        var missingOrderProducts = expectedOrderProducts
            .Where(orderProduct => !existingOrderProductKeys.Contains((orderProduct.OrderId, orderProduct.ProductId)))
            .ToArray();

        if (missingOrderProducts.Length > 0)
        {
            await dbContext.OrderProducts.AddRangeAsync(missingOrderProducts);
            await dbContext.SaveChangesAsync();
        }

        // Commin all changes.
        await transaction.CommitAsync();
    }
}