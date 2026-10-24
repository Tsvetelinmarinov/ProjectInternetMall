using EMall.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

#region Application Construction and IoC Container Configuration

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") 
     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        connectionString,
        (sqlServerDbContextOptionsBuilder) =>
        {
            // For Vector search
            sqlServerDbContextOptionsBuilder.UseCompatibilityLevel(170);

            // For preventing sql injection.
            sqlServerDbContextOptionsBuilder
                .UseParameterizedCollectionMode(ParameterTranslationMode.MultipleParameters);
        }
    );
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

#endregion
#region Configure HTTP Request Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseStatusCodePagesWithReExecute("/Home/Error");
}
else
{
    app.UseStatusCodePagesWithReExecute("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}")
   .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

#endregion
#region Startup

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext dtb = scope
        .ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    try
    {
        dtb.Database.Migrate();
    }
    catch (Exception ex)
    {
        // If something happen while migrating, atleast write it to the console,
        // because the application will still attempt to migrate, so we should know from the beginning.
        Console.WriteLine($"Unsuccessful startup migration! Error message: {ex.Message}\n At: {ex.StackTrace}\n");
    }
}

app.Run();

#endregion