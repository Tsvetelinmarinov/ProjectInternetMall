namespace EMall.Areas.Administration.ViewModels;

public class AdminDashboardViewModel
{
    public int ProductsCount { get; set; }
    public int OrdersCount { get; set; }
    public int PendingOrders { get; set; }
    public int UsersCount { get; set; }
    public int NewUsersThisWeek { get; set; }
    public decimal RevenueThisMonth { get; set; }

    public List<CategoryStatViewModel> Categories { get; set; } = new();
    public List<RecentOrderViewModel> RecentOrders { get; set; } = new();
    public List<LowStockViewModel> LowStock { get; set; } = new();

    // Демо данни, за да видиш панела веднага. Замени с реални заявки към DbContext.
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

public class CategoryStatViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Color { get; set; } = "#2250F5";
    public int ProductsCount { get; set; }
}

public class RecentOrderViewModel
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "Нова";
}

public class LowStockViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}