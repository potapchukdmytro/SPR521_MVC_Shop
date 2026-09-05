using Microsoft.EntityFrameworkCore;
using SPR521_Shop.Models;
using System.Text.Json;

namespace SPR521_Shop.Initializer
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var filePath = Path.Combine(env.WebRootPath, "seedData", "CategoriesAndProducts.json");

                if(!File.Exists(filePath))
                {
                    return;
                }

                var json = File.ReadAllText(filePath);

                var categories = JsonSerializer.Deserialize<List<Category>>(json);

                if (categories != null)
                {
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }

                //List<Category> categories = new List<Category>
                //{
                //    new Category
                //    {
                //        Name = "Процесори (CPU)",
                //        Description = "Центральні процесори для настільних ПК та серверів.",
                //        Image = "793814ed-f6e8-4316-8bcc-76a76a8428e7.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Материнські плати",
                //        Description = "Плти для об'єднання всіх компонентів системи в єдине ціле.",
                //        Image = "48a67e24-95d8-4ac4-8585-aadeb2595877.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Оперативна пам'ять (RAM)",
                //        Description = "Модулі швидкої тимчасової пам'яті (DDR4, DDR5).",
                //        Image = "773d1607-7c00-4544-998f-58ec3c447c61.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Відеокарти (GPU)",
                //        Description = "Графічні адаптери для ігор, рендерингу та обробки графіки.",
                //        Image = "fec4f980-386a-4055-903d-f71c0591a755.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Накопичувачі (SSD / HDD)",
                //        Description = "Твердотільні та жорсткі диски для зберігання даних.",
                //        Image = "f40a292c-6d15-4e09-aed1-4d958c703d10.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Блоки живлення",
                //        Description = "Пристрої для забезпечення стабільного живлення всіх компонентів ПК.",
                //        Image = "a476f861-3a56-470a-856d-5db503bb3011.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Корпуси",
                //        Description = "Захисні шасі для зручного та безпечного розміщення комплектуючих.",
                //        Image = "e8f3861f-224a-49b7-8245-b2808f41831a.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Системи охолодження",
                //        Description = "Кулери, радіатори та комплекти рідинного охолодження (СЖО).",
                //        Image = "31a02bea-1dcf-4aff-8439-d5e0d56c23c2.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Звукові карти",
                //        Description = "Дискретні пристрої для виведення та обробки високоякісного звуку.",
                //        Image = "e25e0281-31b1-4372-a1ca-33d5918a39f6.jpg"
                //    },
                //    new Category
                //    {
                //        Name = "Мережеві адаптери",
                //        Description = "Wi-Fi модулі та дротові мережеві карти (Ethernet) для підключення до інтернету.",
                //        Image = "fa776b25-4db9-4a0f-b942-babb692c663e.jpg"
                //    }
                //};

                //context.Categories.AddRange(categories);
                //context.SaveChanges();
            }
        }
    }
}
