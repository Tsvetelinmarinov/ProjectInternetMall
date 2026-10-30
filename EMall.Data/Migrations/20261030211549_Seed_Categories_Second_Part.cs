using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMall.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Categories_Second_Part : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "Icon" },
                values: new object[,]
                {
                    { 1, "Ултрабуци, офис и гейминг модели за работа, учене и игри.", "Лаптопи", "bi-laptop" },
                    { 2, "Флагмани и достъпни модели с добра камера и батерия за целия ден.", "Смартфони", "bi-phone" },
                    { 3, "Конзоли, мишки, клавиатури и монитори за по-бързи игри.", "Гейминг", "bi-controller" },
                    { 4, "Безжични слушалки, тонколони и саундбарове.", "Аудио", "bi-headphones" },
                    { 5, "Камери, крушки и сензори, които се управляват от телефона.", "Умен Дом", "bi-lightbulb" },
                    { 6, "Зарядни, кабели, калъфи и чанти за твоята техника.", "Аксесоари", "bi-plug" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);
        }
    }
}
