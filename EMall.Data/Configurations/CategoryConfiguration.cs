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
                new Category 
                { 
                    CategoryId = 1, 
                    CategoryName = "Лаптопи",
                    CategoryDescription = "Ултрабуци, офис и гейминг модели за работа, учене и игри.",
                    Icon = "bi-laptop"
                },
                new Category 
                { 
                    CategoryId = 2,
                    CategoryName = "Смартфони",
                    CategoryDescription = "Флагмани и достъпни модели с добра камера и батерия за целия ден.",
                    Icon = "bi-phone"
                },
                new Category 
                {
                    CategoryId = 3,
                    CategoryName = "Гейминг",
                    CategoryDescription = "Конзоли, мишки, клавиатури и монитори за по-бързи игри.",
                    Icon = "bi-controller"

                },
                new Category 
                {
                    CategoryId = 4,
                    CategoryName = "Аудио",
                    CategoryDescription = "Безжични слушалки, тонколони и саундбарове.",
                    Icon = "bi-headphones"
                },
                new Category 
                {
                    CategoryId = 5,
                    CategoryName = "Умен Дом",
                    CategoryDescription = "Камери, крушки и сензори, които се управляват от телефона.",
                    Icon = "bi-lightbulb"
                },
                new Category 
                {
                    CategoryId = 6,
                    CategoryName = "Аксесоари",
                    CategoryDescription = "Зарядни, кабели, калъфи и чанти за твоята техника.",
                    Icon = "bi-plug"
                }
            ]);
        }
    }
}