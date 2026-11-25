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
using EMall.ViewModels.ModelBinders.Providers;

namespace EMall
{
    public class Program
    {
        public static async Task Main()
        {
            #region Application Construction and IoC Container Configuration

            // Create the application builder.
            var builder = WebApplication.CreateBuilder();

            // Get the connection string for the database(SQL Server 2025).
            var connectionString = builder
                .Configuration
                .GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException(NoDbConnectionKey);

            // Add services of controllers and views to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer( //=> We using SQL Server.
                    connectionString,
                    (sqlServerDbContextOptionsBuilder) =>
                    {
                        // For Vector search that will not be implemented, because i cannot
                        // make the lectors/teachers to install locally Ollama model for that.
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

            // Adding the X-Developer header adding filter.
            builder.Services.AddMvc((mvcOptions) =>
            {
                mvcOptions.Filters.Add<AsyncResultFilterForAddingXDeveloperHeader>();
            });

            // Adding model binder for ProductViewModel for correct
            // binding the model from HTTP request.
            builder.Services.AddMvc((mvcOptions) =>
            {
                mvcOptions
                    .ModelBinderProviders
                    .Insert(0, new ProductViewModelModelBinderProvider());
            });

            // Registers IDistributedCache + ISessionStore
            // for caching user data in session cookie.
            builder.Services.AddDistributedMemoryCache();

            // Add sessions cache configuration.
            builder.Services.AddSession((sessionOptions) =>
            {
                sessionOptions.IdleTimeout = TimeSpan.FromHours(1);
                sessionOptions.Cookie.HttpOnly = true;
                sessionOptions.Cookie.IsEssential = true;
            });

            // Build the application host.
            var app = builder.Build();

            #endregion
            #region Configure HTTP Request Pipeline

            // If we are in production - show the production error page for every error/status code.
            if (app.Environment.IsProduction())
            { 
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts(); // Always HTTPS!
            }
            else //=> If we are in development mode, we need information for the errors,
                // and we need it in the browser, so ..
            {
                app.UseDeveloperExceptionPage();
            }

            // If HTTP -> HTTPS, but with HSTS this is not needed.
            app.UseHttpsRedirection();

            // Validate anty-forgery token to preven from CSRF(Cross-Site Request Forgery).
            app.UseAntiforgery();

            // Maps endpoint for files in wwwroot.
            app.MapStaticAssets();

            // Add Microsoft routing system/middleware.
            app.UseRouting();

            // Add default coltures for Bulgaria and England.
            app.UseRequestLocalization((requestOptions) =>
            {
                requestOptions.AddSupportedCultures(["bg-BG", "en-EN"]);
            });

            // Use authentication cookie.
            app.UseAuthentication();

            // Use authorization.
            app.UseAuthorization();

            // Make session/session cookie.
            app.UseSession();

            // Map route with areas.
            app.MapControllerRoute(
                "areas", 
                "{area:exists}/{controller}/{action}/{id?}",
                new { controller = "Home", action = "Index" },
                new { id = @"\d*" }
            ).WithStaticAssets();

            // Map route without areas.
            app.MapControllerRoute(
                "default", 
                "{controller}/{action}/{id?}",
                new { controller = "Home", action = "Index" },
                new { id = @"\d*" }
            ).WithStaticAssets();

            // Map Razor pages, because of Identity system.
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

                await DemoUserSeeder.SeedAsync(
                    usersManager,
                    app.Configuration["DemoUsers:InitialPassword"]
                );
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