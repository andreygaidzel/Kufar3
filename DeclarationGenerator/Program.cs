using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kufar3.Models;
using System.Web;
using System.Security.Cryptography;
using Kufar3.Migrations;


namespace DeclarationGenerator
{
    class Program
    {
        public static KufarContext Context = new KufarContext();
        public static Random R = new Random();

        private static void Main()
        {
            //Configure();
            var declarationCount = (float) 20;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("<Download Declarations>");
            for (int i = 1; i < declarationCount + 1; i++)
            {
                var prosent = i / declarationCount * 100;
                AddDeclaration();
                Console.Write("\rAdd:");
                Console.Write(prosent);
                Console.Write("%");
            }
            Console.WriteLine("\n__Done");
            Console.Read();
        }

        public static void Configure()
        {
            if (Context.Database.Exists())
            {
                Console.WriteLine("exist database");
                Context.Database.Delete();
                Console.WriteLine("Delete");
                Context.Database.Create();
                Console.WriteLine("Create");
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<KufarContext, Configuration>());
            }
        }

        public static void AddDeclaration() 
        {
            var newDeclaration = new Declaration
            {
                Name = NewName(),
                Description = NewDescription(),
                SubCategoryId = R.Next(1, Context.SubCategories.Count()),
                Type = 0,
                UserId = R.Next(1, Context.Users.Count()),
                CityId = R.Next(1, Context.Cities.Count()),
                Price = R.Next(1, 500).ToString(),
                CreatedDate = DateTime.UtcNow
            };

            Context.Declarations.Add(newDeclaration);
            Context.SaveChanges();

            var imgMax = R.Next(1, 6);
            for (int i = 0; i < imgMax; i++)
            {
                var url = UploadImage();

                if (!string.IsNullOrEmpty(url))
                {
                    Context.Images.Add(new Image
                    {
                        Name = url,
                        DeclarationId = newDeclaration.Id,
                    });
                }
            }
            Context.SaveChanges();
        }

        public static string NewDescription()
        {
            var a1 = new[]
            {
                "Товарищ!", "С другой стороны ", "Равным образом ", "Не следует, однако, забыть, что ",
                "Таким образом ", "Повседневная практика показыват, что ",
                "Значимость этих проблем настолько очевидная, что ", "Разнообразный и богатый опыт ",
                "Задача организации, в особенности же ", "Идейные соображения высокого порядка, а также "
            };
            var a2 = new[]
            {
                "реализация намеченных плановых заданий ", "рамки и место обучения кадров ",
                "постоянный количественный рост и сфера нашей активности ", "сложившаеся структура организации ",
                "новая модель организационной деятельности ", "дальнейшее развитие различных форм деятельности ",
                "постоянное информационно-пропагандисткое обеспечение нашей деятельности ",
                "управление и развитие структуры ", "консультация с широким активом ",
                "начало повседневной работы по формированию позиции"
            };
            var a3 = new[]
            {
                "играет важную роль в формировании ", "требует от наз анализа ", "требует определения и уточнения ",
                "способствует подготовке и реализации ", "обеспечивает широкому кругу ", "участие в формировании ",
                "в значительной степени обуславливает создание ",
                "позволяет оценить значение, представляет собой интересный эксперимент ",
                "позволяет выполнять разные задачи ",
                "проверки влечет за собой интересный процесс внедрения и модернизации"
            };
            var a4 = new[]
            {
                "существующим финансовых и административных условий.", "дальнейших направлений развития.",
                "системы массового участия.", "позиций, занимаемых участниками в отношении поставленных задач.",
                "новых предложений.", "направлений прогрессивного развития.",
                "системы обучения кадров, соответствующей насущным потребностям.",
                "соответствующих условий активизации.", "модели развития.", "форм воздействия."
            };

            var description = (a1[R.Next(a1.Length)]) + (a2[R.Next(a2.Length)]) + (a3[R.Next(a3.Length)]) +
                              (a4[R.Next(a4.Length)]);

            return description;
        }

        public static string NewName()
        {
            var b1 = new[]
            {
                "Китайская шуба!", "Горный велосипед", "Новые носки", "Старые коньки", "Синий трактор",
                "Прицеп зубренок 2т", "Незабываемая поездка для всей семьи", "Разнообразный и богатый выбор", "Булочка",
                "Пара кроликов"
            };

            var name = b1[R.Next(b1.Length)];

            return name;
        }

        public static string GetRandomAlphaNumeric()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(chars.Select(c => chars[R.Next(chars.Length)]).Take(16).ToArray());
        }

        public static string UploadImage()
        {
            var path = @"C:\test\" + R.Next(1, 6) + ".jpg";
            var random = GetRandomAlphaNumeric();
            var name = @"\Images\_IMG_" + random + "__testing.jpg";
            var newPath = @"C:\Projects\Kufar3\Kufar3" + name;

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                //fileInf.CopyTo(newPath, true);
                // альтернатива с помощью класса File
                 File.Copy(path, newPath, true);
            }
            return name;
        }
    }
}
