namespace EMall.Areas.Administration.ViewModels;

/// <summary>
///  View model for the administrator bash board.
/// </summary>
public class AdminDashboardViewModel
{
    /// <summary>
    ///  The count of all products.
    /// </summary>
    public int ProductsCount { get; set; }

    /// <summary>
    ///  The count of all orders.
    /// </summary>
    public int OrdersCount { get; set; } 

    /// <summary>
    /// The count of pending orders.
    /// </summary>
    public int PendingOrders { get; set; }

    /// <summary>
    ///  The count of registered users.
    /// </summary>
    public int UsersCount { get; set; }

    /// <summary>
    ///  The count of the registered users this week.
    /// </summary>
    public int NewUsersThisWeek { get; set; }

    /// <summary>
    ///  Total revenue for the mount.
    /// </summary>
    public decimal RevenueThisMonth { get; set; }

    /// <summary>
    ///  Categories.
    /// </summary>
    public List<CategoryStatViewModel> Categories { get; set; } = [];

    /// <summary>
    ///  Recent orders.
    /// </summary>
    public List<RecentOrderViewModel> RecentOrders { get; set; } = [];

    /// <summary>
    ///  Low stock products.
    /// </summary>
    public List<LowStockViewModel> LowStock { get; set; } = [];

    // Mock data for example visualization.
    public static AdminDashboardViewModel Demo() => new()
    {
        ProductsCount = 248,
        OrdersCount = 1382,
        PendingOrders = 7,
        UsersCount = 964,
        NewUsersThisWeek = 23,
        RevenueThisMonth = 18420.50m,
        Categories =
        {
            new() { CategoryId = 1, CategoryName = "Лаптопи",    Color = "#2250F5", ProductsCount = 46 },
            new() { CategoryId = 2, CategoryName = "Смартфони",  Color = "#0FA3B1", ProductsCount = 72 },
            new() { CategoryId = 3, CategoryName = "Гейминг",    Color = "#7B3FE4", ProductsCount = 38 },
            new() { CategoryId = 4, CategoryName = "Аудио",      Color = "#E5484D", ProductsCount = 31 },
            new() { CategoryId = 5, CategoryName = "Умен Дом",   Color = "#2E9E5B", ProductsCount = 24 },
            new() { CategoryId = 6, CategoryName = "Аксесоари",  Color = "#E8A317", ProductsCount = 37 }
        },
        RecentOrders =
        {
            new() { Id = 1382, CustomerEmail = "ivan.petrov@example.com",  CreatedOn = DateTime.Now.AddHours(-2),  Total = 1249.00m, Status = "Нова" },
            new() { Id = 1381, CustomerEmail = "maria.georgieva@example.com", CreatedOn = DateTime.Now.AddHours(-9),  Total = 89.90m,   Status = "Обработва се" },
            new() { Id = 1380, CustomerEmail = "d.stoyanov@example.com",   CreatedOn = DateTime.Now.AddDays(-1),   Total = 459.00m,  Status = "Изпратена" },
            new() { Id = 1379, CustomerEmail = "nina.k@example.com",       CreatedOn = DateTime.Now.AddDays(-2),   Total = 2199.00m, Status = "Доставена" },
            new() { Id = 1378, CustomerEmail = "georgi.h@example.com",     CreatedOn = DateTime.Now.AddDays(-3),   Total = 35.50m,   Status = "Отказана" }
        },
        LowStock =
        {
            new() { ProductId = 12, ProductName = "Безжични слушалки X2", Quantity = 3 },
            new() { ProductId = 57, ProductName = "Геймърска мишка Pro",  Quantity = 2 }
        }
    };
}