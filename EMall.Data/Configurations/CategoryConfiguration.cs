using EMall.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMall.Data.Configurations
{
    /// <summary>
    ///  Seeds data in table Categories.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData([
                new Category { CategoryId = 1, CategoryName = "Лаптопи" },
                new Category { CategoryId = 2, CategoryName = "Смартфони" },
                new Category { CategoryId = 3, CategoryName = "Гейминг" },
                new Category { CategoryId = 4, CategoryName = "Аудио" },
                new Category { CategoryId = 5, CategoryName = "Умен Дом" },
                new Category { CategoryId = 6, CategoryName = "Аксесоари" }
            ]);
        }
    }
}