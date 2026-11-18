using EMall.Data.Configurations;
using EMall.Data.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMall.Data.DatabaseContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                   .ComplexCollection(
                        product => product.Claims,
                        complexCollectionBuilder => complexCollectionBuilder.ToJson()
                   );

            builder.Entity<Product>()
                   .HasQueryFilter("SoftDelete", (product) => product.IsDeleted == false);

            // Apply all the Category entity configuration.
            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);

            base.OnModelCreating(builder);
        }
    }
}