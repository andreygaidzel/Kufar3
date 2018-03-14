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
                    Name = "����",
                    MobileNumber = "+375255212343",
                    Password = "123456",
                    Role = UserRoles.Admin
                });

                context.Users.Add(new User
                {
                    Email = "moderator@gmail.com",
                    Name = "�����",
                    MobileNumber = "+375255212883",
                    Password = "123456",
                    Role = UserRoles.Moderator
                });

                context.Users.Add(new User
                {
                    Email = "user@gmail.com",
                    Name = "�������",
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
                    Name = "���� � �����",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "������� ������"},
                        new SubCategory {Name = "������� ������"},
                        new SubCategory {Name = "������� �����"},
                        new SubCategory {Name = "������� �����"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "������������",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "��������"},
                        new SubCategory {Name = "�������"},
                        new SubCategory {Name = "����"},
                        new SubCategory {Name = "������"},
                        new SubCategory {Name = "�������"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "�������",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "������������"},
                        new SubCategory {Name = "�� � ������������"},
                        new SubCategory {Name = "����������"},
                    }
                });
                context.Categories.Add(new Category
                {
                    Name = "���� � ���������",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory {Name = "�������� ����"},
                        new SubCategory {Name = "���������"},
                        new SubCategory {Name = "���������"},
                        new SubCategory {Name = "��������"},
                    }
                });
                /*********************************************************************************************************************************/

                if (!context.Regions.Any())
                {
                    context.Regions.Add(new Region
                    {
                        Name = "�.�����",
                        Cities = new List<City>
                        {
                            new City {Name = "�����������"},
                            new City {Name = "���������"},
                            new City {Name = "������������"},
                            new City {Name = "����������"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "�������",
                        Cities = new List<City>
                        {
                            new City {Name = "��������"},
                            new City {Name = "�������"},
                            new City {Name = "�������"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "���������",
                        Cities = new List<City>
                        {
                            new City {Name = "�����"},
                            new City {Name = "����������"},
                            new City {Name = "������"},
                            new City {Name = "���������"},
                            new City {Name = "��������"},
                            new City {Name = "�������"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "���������",
                        Cities = new List<City>
                        {
                            new City {Name = "�������"},
                            new City {Name = "�������"},
                            new City {Name = "�����������"},
                            new City {Name = "������������"},
                            new City {Name = "��������"},
                            new City {Name = "�������"},
                            new City {Name = "�������"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "����������",
                        Cities = new List<City>
                        {
                            new City {Name = "������"},
                            new City {Name = "�����"},
                            new City {Name = "������"},
                            new City {Name = "�����"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "�����������",
                        Cities = new List<City>
                        {
                            new City {Name = "������"},
                            new City {Name = "�����������"},
                            new City {Name = "���������"},
                            new City {Name = "��������"},
                        }
                    });
                    context.Regions.Add(new Region
                    {
                        Name = "�����������",
                        Cities = new List<City>
                        {
                            new City {Name = "�������"},
                            new City {Name = "��������"},
                            new City {Name = "��������"},
                            new City {Name = "�����"},
                        }
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}