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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                   .ComplexCollection(
                        product => product.Claims,
                        complexCollectionBuilder => complexCollectionBuilder.ToJson()
                   );

            builder.Entity<Product>()
                   .HasQueryFilter("SoftDelete", (product) => product.IsDeleted == false);

            base.OnModelCreating(builder);
        }
    }
}