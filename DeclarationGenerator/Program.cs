using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using EntityFramework.Utilities;
using Kufar3.Migrations;
using Kufar3.Models;

namespace DeclarationGenerator
{
    class Program
    {
        private static KufarContext Context = new KufarContext();
        private static Random R = new Random();
        private static readonly int _subcategoryCount;
        private static readonly int _userCount;
        private static readonly int _cityCount;

        static Program()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Configure();

            _subcategoryCount = Context.SubCategories.Count();
            _userCount = Context.Users.Count();
            _cityCount = Context.Cities.Count(); 
        }

        public static void Configure()
        {
            if (Context.Database.Exists())
            {
                try
                {
                    Context.Database.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                Console.WriteLine("Database deleted.");
            }

            DatabaseMigrator.Initialize();
            Context.Users.Add(new User()); // TODO: пофиксить

            Console.WriteLine("Database created.");
        }

        private static void Main()
        {
            TimerHelper.StopWatch(Start);

            Console.Read();
        }

        private static void Start()
        {
            const int declarationCount = 1000;

            Console.WriteLine("<Download Declarations>");

            var declarations = new List<Declaration>();

            for (var i = 1; i < declarationCount + 1; i++)
            {
                var declaration = AddDeclaration();
                declarations.Add(declaration);
            }
            
            //EFBatchOperation.For(Context, Context.Declarations).InsertAll(declarations);

            Context.Declarations.AddRange(declarations);
            Context.SaveChanges();

            Console.WriteLine("\n__Done");
        }

        public static Declaration AddDeclaration()
        {
            var images = new List<Image>();
            var imgMax = R.Next(1, 7);
            for (var i = 0; i < imgMax; i++)
            {
                var url = UploadImage();

                if (!string.IsNullOrEmpty(url))
                {
                    images.Add(new Image
                    {
                        Name = url,
                    });
                }
            }

            var newDeclaration = new Declaration
            {
                Name = NewName(),
                Description = NewDescription(),
                SubCategoryId = R.Next(1, _subcategoryCount + 1),
                Type = (DeclarationTypes)R.Next(0, Enum.GetNames(typeof(DeclarationTypes)).Length),
                UserId = R.Next(1, _userCount + 1),
                CityId = R.Next(1, _cityCount + 1),
                Price = R.Next(1, 501),
                CreatedDate = RandomDay(),
                Images = images
            };

            return newDeclaration;
        }
        
        public static DateTime RandomDay()
        {
            var start = new DateTime(2017, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(R.Next(range));
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

            return a1[R.Next(a1.Length)] + a2[R.Next(a2.Length)] + a3[R.Next(a3.Length)] +
                   a4[R.Next(a4.Length)];
        }

        public static string NewName()
        {
            var b1 = new[]
            {
                "Китайская шуба!", "Горный велосипед", "Новые носки", "Старые коньки", "Синий трактор",
                "Прицеп зубренок 2т", "Незабываемая поездка для всей семьи", "Разнообразный и богатый выбор", "Булочка",
                "Пара кроликов"
            };

            return b1[R.Next(b1.Length)];
        }
        
        public static string UploadImage()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var path = @"TestImages\" + R.Next(1, 6) + ".jpg";
            var random = Guid.NewGuid().ToString("n");
            var name = @"/Images/_IMG_" + random + "__testing.jpg";
            var newPath = @"C:/Projects/Kufar3-last/Kufar3" + name;

            var fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                File.Copy(path, newPath, true);
            }

            return name;
        }
    }
}