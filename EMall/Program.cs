using EMall.Data;
using EMall.Data.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EMall
{
    public class Program
    {
        public static async Task Main()
        {
            #region Application Construction and IoC Container Configuration

            var builder = WebApplication.CreateBuilder();

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
                .AddDefaultIdentity<ApplicationUser>(
                    options => ConfigureIdentity(options, builder.Configuration)
                )
                .AddRoles<IdentityRole>()
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

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}")
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
                    
                    return; //=> Because, if we do not migrate, table AspNetRoles will not exist,
                           //=> so the code bellow will throw some type of Exception.
                }
            }

            // If database exists, add role Admin in AspNetRoles.
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var rolesManager = scope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                string adminRole = "Administrator"; //=> System admin.

                try
                {
                    if (await rolesManager.RoleExistsAsync(adminRole) is false)
                    {
                        var roleCreatingResult = await rolesManager.CreateAsync(new IdentityRole(adminRole));

                        if (roleCreatingResult.Succeeded)
                        {
                            Console.WriteLine("Successfully created role {0}!", adminRole);
                        }
                    }
                }
                catch
                {
                    Debug.WriteLine($"Unable to create role {adminRole} in table AspNetRoles, because of system error.");
                }
            }

            app.Run();

            #endregion
        }

        #region Private Functionality

        // Configures the default Identity.
        private static void ConfigureIdentity(IdentityOptions options, ConfigurationManager configuration)
        {
            // User settings
            options.User.RequireUniqueEmail =
                configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");

            // Sign in settings.
            options.SignIn.RequireConfirmedAccount =
                configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

            // Lockout settings.
            options.Lockout.AllowedForNewUsers =
                configuration.GetValue<bool>("Identity:Lockout:AllowedForNewUsers");

            options.Lockout.MaxFailedAccessAttempts =
                configuration.GetValue<int>("Identity:Lockout:MaxFailedAccessAttempts");

            options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(configuration.GetValue<int>("Identity:Lockout:DefaultLockoutTimeSpan"));

            // Password settings.
            options.Password.RequiredLength =
                configuration.GetValue<int>("Identity:Password:RequiredLength");

            options.Password.RequiredUniqueChars =
                configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");

            options.Password.RequireDigit =
                configuration.GetValue<bool>("Identity:Password:RequireDigit");

            options.Password.RequireLowercase =
                configuration.GetValue<bool>("Identity:Password:RequireLowercase");

            options.Password.RequireUppercase =
                configuration.GetValue<bool>("Identity:Password:RequireUppercase");

            options.Password.RequireNonAlphanumeric =
                configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
        }

        #endregion
    }
}