using EMall.Core.Services;
using EMall.Core.Services.Interfaces;
using EMall.Core.AutoMapper;
using EMall.Data;
using EMall.Data.SeedData;
using EMall.Data.DatabaseContext;
using EMall.Data.Repositories;
using EMall.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EMall.AutoMapper;
using static EMall.Constants.WebConstants.Common;
using EMall.PipelineFilters.ResultFilters;
using EMall.PipelineFilters.ResultFilters.Options;

namespace EMall
{
    public class Program
    {
        public static async Task Main()
        {
            #region Application Construction and IoC Container Configuration

            var builder = WebApplication.CreateBuilder();

            var connectionString = builder
                .Configuration
                .GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException(NoDbConnectionKey);

            // Add services of controllers and views to the container +
            // services for exception page for database errors.
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();

            // Add DbContext.
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

            // Add Identity system.
            builder.Services
                .AddDefaultIdentity<ApplicationUser>(
                    options => ConfigureIdentity(options, builder.Configuration)
                ).AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add Repositories.
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            // Add Services.
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            // Add AutoMapper.
            builder.Services.AddAutoMapper((mappingConfig) =>
            {
                mappingConfig.AddMaps(
                    [
                        typeof(CategoryMappingProfile).Assembly,
                        typeof(CategoryServiceMappingProfile).Assembly
                    ]
                );
            });

            // Add the XDeveloperHeaderOptions that holds the developer name 
            // from the configuration for the X-Developer response header.
            //
            // Този прост филтър(който добавя хедъра), въобще като цяло използва
            // конфигурацията за името и опцийте му имплементират шаблона на опцийте,
            // за да покажа разбиране по темата.
            builder.Services.Configure<XDeveloperHeaderOptions>((xDevHeaderOptions) =>
            {
                xDevHeaderOptions.DeveloperFullName 
                    = builder.Configuration["DefinedHeaders:X-Developer"] 
                      ?? NoConfiguredDeveloperNameForXDeveloper;
            });

            // Build the application host.
            var app = builder.Build();

            #endregion
            #region Configure HTTP Request Pipeline

            if (app.Environment.IsProduction())
            { 
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                "areas", 
                "{area:exists}/{controller}/{action}/{id?}",
                new { controller = "Home", action = "Index" },
                new { id = @"\d*" }
            ).WithStaticAssets();

            app.MapControllerRoute(
                "default", 
                "{controller}/{action}/{id?}",
                new { controller = "Home", action = "Index" },
                new { id = @"\d*" }
            ).WithStaticAssets();

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
                    await AdminDashboardDataSeeder.SeedAsync(dtb); //=> If no products -> seed any.
                }
                catch (Exception ex)
                {
                    // If something happen while migrating, atleast write it to the console,
                    // because the application will still attempt to migrate, so we should know from the beginning.
                    Console.WriteLine(UnableToMigrateOnStartUp, ex.Message, ex.StackTrace);
                    
                    return; //=> Because, if we do not migrate, table AspNetRoles will not exist,
                           //=> so the code bellow will throw some type of Exception.
                }
            }

            // If database exists, add role Administrator and default admin user - me + role User.
            using (IServiceScope scope = app.Services.CreateScope())
            {
                // Manager of roles.
                var rolesManager = scope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                // Manager of users.
                var usersManager = scope
                    .ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                string adminRole = "Administrator"; //=> System admin.
                string userRole = "User"; //=> System user.

                try
                {
                    // Check if the admin role exist, and if not - create it.
                    if (await rolesManager.RoleExistsAsync(adminRole) is false)
                    {
                        var roleCreatingResult 
                            = await rolesManager.CreateAsync(new IdentityRole(adminRole));

                        if (roleCreatingResult.Succeeded)
                        {
                            Console.WriteLine(SuccessfullyCreateRole, adminRole);
                        }
                    }

                    // Check if the role User exists, and if not - create it.
                    if (await rolesManager.RoleExistsAsync(userRole) is false)
                    {
                        var userRoleCreatingResult
                            = await rolesManager.CreateAsync(new IdentityRole(userRole));

                        if (userRoleCreatingResult.Succeeded)
                        {
                            Console.WriteLine(SuccessfullyCreateRole, userRole);
                        }
                    }

                    // Specify the email of the administrator.
                    var adminEmail 
                        = app.Configuration.GetValue<string>("AdminProfile:Email")
                            ?? throw new InvalidOperationException(NoAdminEmail);

                    // Find the user with that email.
                    var admin = await usersManager.FindByEmailAsync(adminEmail);

                    // If does not exists - create it.
                    if (admin is null)
                    {
                        admin = new ApplicationUser 
                        { 
                            UserName = adminEmail,
                            Email = adminEmail,
                            EmailConfirmed = true
                        };

                        var adminPassword 
                            = app.Configuration.GetValue<string>("AdminProfile:Password")
                                ?? throw new InvalidOperationException(NoAdminPassword);

                        var adminCreatingResult 
                            = await usersManager.CreateAsync(admin, adminPassword);

                        // Check for succeeded creating, and if true - print message for success in the Console.
                        if (adminCreatingResult.Succeeded is true)
                        {
                            Console.WriteLine(SuccessAdminCreate, adminEmail);
                        }
                    }

                    // If the new user is not admin yet, make it admin.
                    if (await usersManager.IsInRoleAsync(admin, adminRole) is false) 
                    {
                        await usersManager.AddToRoleAsync(admin, adminRole);
                    }
                }
                catch
                {
                    Console.WriteLine(UnableToCreateAdminRole, adminRole);
                }
            }

            app.Run();

            #endregion
        }

        #region Private Functionality

        // Configures the Identity system.
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