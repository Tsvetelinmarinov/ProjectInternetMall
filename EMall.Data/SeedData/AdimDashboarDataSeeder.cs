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

        // Find existing baseline products so missing seed data can be added safely.
        var existingSeededProducts = await dbContext
            .Products
            .Where(product => seededProductNames.Contains(product.ProductName))
            .ToListAsync();

        var seedProductsDictionary = seedProducts.ToDictionary(product => product.ProductName);

        foreach (var existingSeededProduct in existingSeededProducts)
        {
            existingSeededProduct.Claims = seedProductsDictionary[existingSeededProduct.ProductName].Claims;
        }

        var existingSeededProductNames = existingSeededProducts
            .Select(product => product.ProductName)
            .ToHashSet();

        var missingSeededProducts = seedProducts
            .Where(product => !existingSeededProductNames.Contains(product.ProductName))
            .ToArray();

        if (missingSeededProducts.Length > 0)
        {
            await dbContext.Products.AddRangeAsync(missingSeededProducts);
        }

        await dbContext.SaveChangesAsync();

        seedProducts = await dbContext
            .Products
            .Where(product => seededProductNames.Contains(product.ProductName))
            .ToArrayAsync();

        if (seedProducts.Length != seededProductNames.Length)
        {
            throw new InvalidOperationException(InvalidSeedProductsGrouping);
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

        await SeedAdditionalProductsAndOrdersAsync(dbContext);

        // Commin all changes.
        await transaction.CommitAsync();
    }

    private static async Task SeedAdditionalProductsAndOrdersAsync(ApplicationDbContext dbContext)
    {
        var productSeeds = GetAdditionalProductSeeds();
        var productNames = productSeeds.Select(seed => seed.Name).ToArray();

        var productsByName = await dbContext
            .Products
            .Where(product => productNames.Contains(product.ProductName))
            .ToDictionaryAsync(product => product.ProductName);

        foreach (var seed in productSeeds)
        {
            if (!productsByName.TryGetValue(seed.Name, out var existingProduct))
            {
                var newProduct = CreateProduct(seed);
                await dbContext.Products.AddAsync(newProduct);
                productsByName.Add(seed.Name, newProduct);
                continue;
            }

            existingProduct.Description = seed.Description;
            existingProduct.Price = seed.Price;
            existingProduct.CategoryId = seed.CategoryId;
            existingProduct.Claims = CreateClaims(seed);
        }

        await dbContext.SaveChangesAsync();

        var customerSeeds = DemoCustomerSeeds.All;
        var statuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
        var additionalProducts = productSeeds
            .Select(seed => productsByName[seed.Name])
            .ToArray();

        var orderSeeds = Enumerable.Range(0, customerSeeds.Count)
            .Select(index =>
            {
                var orderProducts = Enumerable.Range(0, 6)
                    .Select(offset => additionalProducts[(index * 3 + offset) % additionalProducts.Length])
                    .ToArray();

                return new Order
                {
                    CustomerName = customerSeeds[index].Name,
                    CustomerEmail = customerSeeds[index].Email,
                    AddressForShipping = $"ул. Примерна {index + 1}, София",
                    CreatedOn = new DateTime(2026, 11, 1, 10, 0, 0).AddDays(index),
                    Status = statuses[index % statuses.Length],
                    TotalPrice = orderProducts.Sum(product => (decimal)product.Price)
                };
            })
            .ToArray();

        var orderEmails = orderSeeds.Select(order => order.CustomerEmail).ToArray();
        var existingOrdersByEmail = await dbContext
            .Orders
            .Where(order => orderEmails.Contains(order.CustomerEmail))
            .ToDictionaryAsync(order => order.CustomerEmail);

        foreach (var orderSeed in orderSeeds)
        {
            if (!existingOrdersByEmail.ContainsKey(orderSeed.CustomerEmail))
            {
                await dbContext.Orders.AddAsync(orderSeed);
                existingOrdersByEmail.Add(orderSeed.CustomerEmail, orderSeed);
            }
        }

        await dbContext.SaveChangesAsync();

        var orderIdsByEmail = existingOrdersByEmail
            .ToDictionary(pair => pair.Key, pair => pair.Value.OrderId);

        var expectedOrderProducts = orderSeeds
            .SelectMany((orderSeed, index) => Enumerable.Range(0, 6)
                .Select(offset => new OrderProduct
                {
                    OrderId = orderIdsByEmail[orderSeed.CustomerEmail],
                    ProductId = additionalProducts[(index * 3 + offset) % additionalProducts.Length].ProductId
                }))
            .ToArray();

        var orderIds = orderIdsByEmail.Values.ToArray();

        var existingOrderProductKeys = await dbContext
            .OrderProducts
            .Where(orderProduct => orderIds.Contains(orderProduct.OrderId))
            .Select(orderProduct => new { orderProduct.OrderId, orderProduct.ProductId })
            .ToListAsync();

        var existingKeys = existingOrderProductKeys
            .Select(orderProduct => (orderProduct.OrderId, orderProduct.ProductId))
            .ToHashSet();

        var missingOrderProducts = expectedOrderProducts
            .Where(orderProduct => !existingKeys.Contains((orderProduct.OrderId, orderProduct.ProductId)))
            .ToArray();

        if (missingOrderProducts.Length > 0)
        {
            await dbContext.OrderProducts.AddRangeAsync(missingOrderProducts);
            await dbContext.SaveChangesAsync();
        }
    }

    private static Product CreateProduct(ProductSeed seed)
        => new()
        {
            ProductName = seed.Name,
            Description = seed.Description,
            Price = seed.Price,
            CategoryId = seed.CategoryId,
            Claims = CreateClaims(seed)
        };

    private static IList<ProductClaim> CreateClaims(ProductSeed seed)
    {
        var claimNames = seed.CategoryId switch
        {
            1 => new[] { "Памет", "Диск", "Процесор", "Екран", "Графика", "Батерия", "Свързване", "Операционна система" },
            2 => new[] { "Памет", "Дисплей", "Процесор", "Основна камера", "Батерия", "Зареждане", "Операционна система", "Защита" },
            3 => new[] { "Тип", "Свързване", "Сензор", "Изпълнение", "Захранване", "Материал", "Съвместимост", "Допълнително" },
            4 => new[] { "Тип", "Свързване", "Звук", "Батерия", "Микрофон", "Материал", "Съвместимост", "Допълнително" },
            5 => new[] { "Тип", "Свързване", "Резолюция", "Захранване", "Управление", "Съвместимост", "Размер", "Допълнително" },
            6 => new[] { "Тип", "Интерфейс", "Мощност", "Размер", "Материал", "Съвместимост", "Захранване", "Допълнително" },
            _ => throw new InvalidOperationException($"Unsupported seed product category: {seed.CategoryId}.")
        };

        if (seed.ClaimValues.Length != claimNames.Length)
        {
            throw new InvalidOperationException($"Seed product '{seed.Name}' must have exactly eight claims.");
        }

        return [.. claimNames.Select((claimName, index) => new ProductClaim
        {
            ClaimName = claimName,
            ClaimValue = seed.ClaimValues[index]
        })];
    }

    private static ProductSeed[] GetAdditionalProductSeeds()
        =>
        [
            new("Acer Aspire 5 A515", "Универсален лаптоп за обучение, офис задачи и ежедневна употреба.", 1199.99, 1, ["16 GB RAM", "512 GB SSD", "Intel Core i5-1335U", "15,6 инча Full HD IPS", "Intel Iris Xe", "До 8 часа", "Wi-Fi 6 и Bluetooth 5.1", "Windows 11 Home"]),
            new("HP Pavilion Plus 14", "Компактен лаптоп с детайлен дисплей за работа и пътуване.", 1849.99, 1, ["16 GB RAM", "1 TB SSD", "Intel Core i7-1355U", "14 инча 2.8K OLED", "Intel Iris Xe", "До 9 часа", "Wi-Fi 6E и Bluetooth 5.3", "Windows 11 Home"]),
            new("Dell Inspiron 16 5640", "Голям лаптоп за продуктивност, видео и домашен офис.", 1699.99, 1, ["16 GB RAM", "1 TB SSD", "Intel Core 7 150U", "16 инча Full HD+", "Intel Graphics", "До 8 часа", "Wi-Fi 6 и Bluetooth 5.2", "Windows 11 Home"]),
            new("ASUS Zenbook 14 OLED", "Тънък ултрабук с OLED екран и алуминиев корпус.", 2299.99, 1, ["16 GB RAM", "1 TB SSD", "Intel Core Ultra 7", "14 инча 2.8K OLED", "Intel Arc Graphics", "До 12 часа", "Wi-Fi 6E и Bluetooth 5.3", "Windows 11 Home"]),
            new("Lenovo LOQ 15 Gaming", "Гейминг лаптоп с бърз дисплей и отделна видеокарта.", 2599.99, 1, ["16 GB RAM", "1 TB SSD", "AMD Ryzen 7 7840HS", "15,6 инча Full HD, 144 Hz", "GeForce RTX 4060 8 GB", "До 6 часа", "Wi-Fi 6 и Bluetooth 5.2", "Windows 11 Home"]),
            new("Google Pixel 9", "Смартфон с интелигентни функции и ярък OLED дисплей.", 1599.99, 2, ["128 GB", "6,3 инча OLED, 120 Hz", "Google Tensor G4", "50 MP с оптична стабилизация", "4700 mAh", "Бързо USB-C зареждане", "Android 15", "IP68"]),
            new("Apple iPhone 16", "Смартфон с бърз процесор и усъвършенствана двойна камера.", 1899.99, 2, ["128 GB", "6,1 инча Super Retina XDR", "Apple A18", "48 MP Fusion камера", "До 22 часа видео", "MagSafe и USB-C", "iOS 18", "IP68"]),
            new("OnePlus 13R", "Бърз смартфон с голяма батерия и плавен AMOLED екран.", 1199.99, 2, ["256 GB", "6,78 инча AMOLED, 120 Hz", "Snapdragon 8 Gen 3", "50 MP основна камера", "6000 mAh", "Бързо зареждане 80 W", "OxygenOS 15", "Защита от пръски"]),
            new("Motorola Edge 50 Neo", "Лек смартфон с ярък дисплей и телефото камера.", 799.99, 2, ["256 GB", "6,4 инча pOLED, 120 Hz", "MediaTek Dimensity 7300", "50 MP с оптична стабилизация", "4310 mAh", "Зареждане 68 W", "Android 14", "IP68"]),
            new("Nothing Phone (2a)", "Смартфон с отличителен дизайн и изчистен интерфейс.", 649.99, 2, ["256 GB", "6,7 инча AMOLED, 120 Hz", "MediaTek Dimensity 7200 Pro", "50 MP двойна камера", "5000 mAh", "Зареждане до 45 W", "Nothing OS 2.5", "Защита IP54"]),
            new("Razer DeathAdder V3", "Ергономична гейминг мишка с лек корпус и прецизен сензор.", 149.99, 3, ["Жична гейминг мишка", "USB-A кабел", "Оптичен сензор Focus Pro", "До 30 000 DPI", "Захранване през USB", "Лек корпус 59 g", "Windows и macOS", "Шест програмируеми бутона"]),
            new("SteelSeries Apex 7", "Механична клавиатура с OLED дисплей и RGB подсветка.", 389.99, 3, ["Механична клавиатура", "USB-A, кабел 1,8 m", "Превключватели Red", "Честота 1000 Hz", "USB захранване", "Алуминиева рамка", "Windows и macOS", "OLED дисплей и RGB"]),
            new("Corsair HS80 RGB", "Безжични гейминг слушалки с пространствен звук.", 299.99, 3, ["Безжични слушалки", "2,4 GHz и Bluetooth", "Dolby Atmos звук", "До 24 часа работа", "Подвижен микрофон", "Меки наушници от плат", "PC, PS5 и Mac", "RGB осветление"]),
            new("Logitech G923 Racing Wheel", "Волан с обратна връзка за реалистични състезателни игри.", 749.99, 3, ["Волан и педали", "USB връзка", "TrueForce обратна връзка", "900 градуса завъртане", "Захранващ адаптер", "Кожена облицовка", "PC, PlayStation 4 и 5", "Двойно съединителен педал"]),
            new("Xbox Wireless Controller", "Безжичен контролер с текстурирани повърхности и споделяне.", 139.99, 3, ["Безжичен контролер", "Bluetooth и Xbox Wireless", "Хибриден D-pad", "Ниска латентност", "2 батерии AA или USB-C", "Текстурирани ръкохватки", "Xbox и Windows PC", "Бутон за споделяне"]),
            new("Bose QuietComfort Ultra", "Премиум слушалки с активно шумопотискане и пространствен звук.", 899.99, 4, ["Безжични слушалки", "Bluetooth 5.3", "Immersive Audio", "До 24 часа слушане", "Вградени микрофони", "Меки наушници", "iOS и Android", "USB-C и аудио кабел"]),
            new("JBL Tune 770NC", "Сгъваеми безжични слушалки с адаптивно шумопотискане.", 199.99, 4, ["Над ушите, безжични", "Bluetooth 5.3", "JBL Pure Bass", "До 70 часа слушане", "Вграден микрофон", "Сгъваем пластмасов корпус", "Телефони и таблети", "Адаптивно шумопотискане"]),
            new("Sennheiser Momentum 4", "Слушалки с детайлно звучене и продължителна автономност.", 699.99, 4, ["Безжични слушалки", "Bluetooth 5.2", "Sennheiser Signature Sound", "До 60 часа слушане", "Четири микрофона", "Сгъваем комфортен дизайн", "iOS, Android и PC", "Адаптивно шумопотискане"]),
            new("Marshall Emberton II", "Компактна преносима тонколона със стерео звук и здрав корпус.", 299.99, 4, ["Преносима тонколона", "Bluetooth 5.1", "360-градусов стерео звук", "Над 30 часа работа", "Без микрофон", "Силиконов корпус", "Телефони и таблети", "Водоустойчивост IP67"]),
            new("Sonos Era 100", "Домашна Wi-Fi тонколона с богат звук и гласово управление.", 499.99, 4, ["Домашна смарт тонколона", "Wi-Fi и Bluetooth", "Стерео звук с два туитъра", "Захранване от електрическата мрежа", "Вградени микрофони", "Компактен матов корпус", "AirPlay 2 и Sonos app", "Автоматична настройка Trueplay"]),
            new("Google Nest Hub 2", "Интелигентен дисплей за управление на дома и ежедневни задачи.", 249.99, 5, ["Смарт дисплей", "Wi-Fi и Bluetooth", "7-инчов сензорен екран", "Захранващ адаптер", "Google Home приложение", "Съвместим с Matter", "Компактен настолен дизайн", "Гласов асистент Google"]),
            new("Eufy Indoor Cam C120", "Вътрешна камера с висока резолюция и автоматично проследяване.", 99.99, 5, ["Вътрешна охранителна камера", "Wi-Fi 2,4 GHz", "2K резолюция", "USB захранване", "Приложение Eufy Security", "Alexa и Google Assistant", "Настолна или стенна стойка", "Откриване на движение"]),
            new("Xiaomi Smart Air Purifier 4", "Пречиствател за въздух с автоматичен режим и сензор за качество.", 499.99, 5, ["Пречиствател за въздух", "Wi-Fi и Bluetooth", "CADR до 400 m³/h", "Мощност 30 W", "Приложение Xiaomi Home", "Google Assistant и Alexa", "За помещения до 48 m²", "HEPA филтър и дисплей"]),
            new("Tapo P110 Smart Plug", "Умен контакт с измерване на консумацията и график.", 39.99, 5, ["Умен електрически контакт", "Wi-Fi 2,4 GHz", "Максимално 16 A", "230 V AC", "Приложение Tapo", "Alexa и Google Assistant", "Компактен корпус", "График и измерване на енергията"]),
            new("Philips Hue Bridge", "Централен контролер за умно осветление и автоматизации.", 119.99, 5, ["Контролер за осветление", "Ethernet и Zigbee", "До 50 осветителни тела", "Захранване 5 V", "Приложение Philips Hue", "Apple Home и Google Home", "Компактен настолен модул", "Поддръжка на Matter"]),
            new("Anker 737 Power Bank", "Външна батерия с голям капацитет и бързо USB-C зареждане.", 249.99, 6, ["Външна батерия", "2 USB-C и 1 USB-A", "Капацитет 24 000 mAh", "Размер 156 × 55 × 50 mm", "Алуминиев корпус", "Телефони и лаптопи", "Зареждане през USB-C", "Дисплей за мощността"]),
            new("Samsung T7 Shield 1TB", "Преносим SSD диск с висока скорост и устойчив корпус.", 279.99, 6, ["Преносим SSD диск", "USB-C 3.2 Gen 2", "До 1050 MB/s четене", "Капацитет 1 TB", "Гумиран удароустойчив корпус", "Windows, Mac и Android", "Захранване през USB", "Защита IP65"]),
            new("Belkin BoostCharge 3-in-1", "Безжична зарядна станция за телефон, часовник и слушалки.", 229.99, 6, ["Безжична зарядна станция", "Qi2 и магнитно закрепване", "До 15 W за телефон", "Сгъваем настолен дизайн", "Корпус от рециклирани материали", "iPhone, Apple Watch и AirPods", "USB-C адаптер включен", "Едновременно зареждане на три устройства"]),
            new("Logitech MX Anywhere 3S", "Компактна безжична мишка за работа на различни повърхности.", 189.99, 6, ["Безжична офис мишка", "Bluetooth и USB приемник", "Сензор до 8000 DPI", "Размер 100 × 65 mm", "Рециклирана пластмаса", "Windows, Mac и ChromeOS", "USB-C презареждане", "Тихи бутони и MagSpeed скрол"]),
            new("UGREEN Revodok 6-in-1", "Компактен USB-C хъб за свързване на периферия и дисплей.", 89.99, 6, ["USB-C мултипорт хъб", "USB-C към HDMI и USB-A", "HDMI до 4K при 60 Hz", "Кабел 15 cm", "Алуминиев корпус", "Лаптопи с USB-C", "До 100 W Power Delivery", "Четец за SD и microSD карти"])
        ];

    private sealed record ProductSeed(
        string Name,
        string Description,
        double Price,
        int CategoryId,
        string[] ClaimValues
    );
}