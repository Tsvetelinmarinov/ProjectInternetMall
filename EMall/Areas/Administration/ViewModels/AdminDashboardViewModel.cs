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
}