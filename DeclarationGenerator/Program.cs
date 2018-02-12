using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kufar3.Models;
using System.Web;

namespace DeclarationGenerator
{
    class Program
    {
        public static KufarContext Context = new KufarContext();
        static void Main()
        {
            //string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.Write("Add:");

            for (int i = 1; i < 21; i++)
            {
                AddDeclaration();
                Console.Write(i);
            }
            
            Console.ReadLine();
        }

        public static void AddDeclaration() 
        {

            var b1 = new string[10] { "Китайская шуба!", "Горный велосипед", "Новые носки", "Старые коньки", "Синий трактор", "Прицеп зубренок 2т", "Незабываемая поездка для всей семьи", "Разнообразный и богатый выбор", "Булочка", "Пара кроликов" };

            var a1 = new string[10] { "Товарищ!", "С другой стороны ", "Равным образом ", "Не следует, однако, забыть, что ", "Таким образом ", "Повседневная практика показыват, что ", "Значимость этих проблем настолько очевидная, что ", "Разнообразный и богатый опыт ", "Задача организации, в особенности же ", "Идейные соображения высокого порядка, а также " };
            var a2 = new string[10] { "реализация намеченных плановых заданий ", "рамки и место обучения кадров ", "постоянный количественный рост и сфера нашей активности ", "сложившаеся структура организации ", "новая модель организационной деятельности ", "дальнейшее развитие различных форм деятельности ", "постоянное информационно-пропагандисткое обеспечение нашей деятельности ", "управление и развитие структуры ", "консультация с широким активом ", "начало повседневной работы по формированию позиции" };
            var a3 = new string[10] { "играет важную роль в формировании ", "требует от наз анализа ", "требует определения и уточнения ", "способствует подготовке и реализации ", "обеспечивает широкому кругу ", "участие в формировании ", "в значительной степени обуславливает создание ", "позволяет оценить значение, представляет собой интересный эксперимент ", "позволяет выполнять разные задачи ", "проверки влечет за собой интересный процесс внедрения и модернизации" };
            var a4 = new string[10] { "существующим финансовых и административных условий.", "дальнейших направлений развития.", "системы массового участия.", "позиций, занимаемых участниками в отношении поставленных задач.", "новых предложений.", "направлений прогрессивного развития.", "системы обучения кадров, соответствующей насущным потребностям.", "соответствующих условий активизации.", "модели развития.", "форм воздействия." };
            Random r = new Random();

            var name = (b1[r.Next(b1.Length)]);
            var description = (a1[r.Next(a1.Length)]) + (a2[r.Next(a2.Length)]) + (a3[r.Next(a3.Length)]) + (a4[r.Next(a4.Length)]);
            var subCategoryId = r.Next(1, 16);
            var userId = r.Next(1, 3);
            var cityId = r.Next(1, 32);

            var newDeclaration = new Declaration
            {
                Name = name,
                Description = description,
                SubCategoryId = subCategoryId,
                Type = DeclarationTypes.Active,
                UserId = userId,
                CityId = cityId,
                Price = r.Next(1, 500).ToString(),
            };

            Context.Declarations.Add(newDeclaration);
            Context.SaveChanges();

            var url = UploadImage();

            if (!string.IsNullOrEmpty(url))
            {
                Context.Images.Add(new Image
                {
                    Name = url,
                    DeclarationId = newDeclaration.Id,
                });
                Context.SaveChanges();
            }
        }

        public static string UploadImage()
        {
            Random r = new Random();
            string path = @"C:\test\" + r.Next(6) + ".jpg";

            var random = Guid.NewGuid().ToString("n");
            string name = @"\Images\_IMG_" + random + "__testing.jpg";
            string newPath = @"C:\Projects\Kufar3\Kufar3" + name;

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
                // альтернатива с помощью класса File
                // File.Copy(path, newPath, true);
            }
            return name;
        }
    }
}
