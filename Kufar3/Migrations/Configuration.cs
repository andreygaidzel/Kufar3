using System.Collections.Generic;
using Kufar3.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Kufar3.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Kufar3.Models.KufarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Kufar3.Models.KufarContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Email = "admin@gmail.com",
                    Name = "Иван",
                    MobileNumber = "+375255212343",
                    Password = "123456",
                    Role = UserRoles.Admin
                });

                context.Users.Add(new User
                {
                    Email = "moderator@gmail.com",
                    Name = "Роман",
                    MobileNumber = "+375255212883",
                    Password = "123456",
                    Role = UserRoles.Moderator
                });

                context.Users.Add(new User
                {
                    Email = "user@gmail.com",
                    Name = "Дмитрий",
                    MobileNumber = "+375255212311",
                    Password = "123456",
                    Role = UserRoles.User
                });

                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category
                {
                    Name = "Мода и стиль",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "Мужская одежда"},
                        new SubCategory {Name = "Женская одежда"},
                        new SubCategory {Name = "Мужская обувь"},
                        new SubCategory {Name = "Женская обувь"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "Недвижимость",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "Квартиры"},
                        new SubCategory {Name = "Комнаты"},
                        new SubCategory {Name = "Дома"},
                        new SubCategory {Name = "Гаражи"},
                        new SubCategory {Name = "Участки"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "Техника",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "Аудиотехника"},
                        new SubCategory {Name = "ТВ и видеотехника"},
                        new SubCategory {Name = "Компьютеры"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "Авто и транспорт",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "Легковые авто"},
                        new SubCategory {Name = "Грузовики"},
                        new SubCategory {Name = "Мотоциклы"},
                        new SubCategory {Name = "Тракторы"},
                    }
                });
                /*********************************************************************************************************************************/

                if (!context.Regions.Any())
                {
                    context.Regions.Add(new Region
                    {
                        Name = "г.Минск",
                        Cities = new List<City>
                        {
                            new City {Name = "Центральный"},
                            new City {Name = "Советский"},
                            new City {Name = "Октяборьский"},
                            new City {Name = "Московский"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Минская",
                        Cities = new List<City>
                        {
                            new City {Name = "Березино"},
                            new City {Name = "Борисов"},
                            new City {Name = "Вилейка"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Брестская",
                        Cities = new List<City>
                        {
                            new City {Name = "Брест"},
                            new City {Name = "Барановичи"},
                            new City {Name = "Береза"},
                            new City {Name = "Ганцевичи"},
                            new City {Name = "Дрогичин"},
                            new City {Name = "Жабинка"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Витебская",
                        Cities = new List<City>
                        {
                            new City {Name = "Витебск"},
                            new City {Name = "Браслав"},
                            new City {Name = "Бешенковичи"},
                            new City {Name = "Верхнедвинск"},
                            new City {Name = "Глубокое"},
                            new City {Name = "Городок"},
                            new City {Name = "Докшицы"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Гомельская",
                        Cities = new List<City>
                        {
                            new City {Name = "Гомель"},
                            new City {Name = "Ветка"},
                            new City {Name = "Добруш"},
                            new City {Name = "Ельск"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Гродненская",
                        Cities = new List<City>
                        {
                            new City {Name = "Гродно"},
                            new City {Name = "Берестовица"},
                            new City {Name = "Волковыск"},
                            new City {Name = "Вороново"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "Могилевская",
                        Cities = new List<City>
                        {
                            new City {Name = "Могилев"},
                            new City {Name = "Бельничи"},
                            new City {Name = "Бобруйск"},
                            new City {Name = "Быхов"},
                        }
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}