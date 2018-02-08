using System.Collections.Generic;
using Kufar3.Models;

namespace Kufar3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Kufar3.Models.KufarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Kufar3.Models.KufarContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role {UserRole = UserRoles.Admin});
                context.Roles.Add(new Role {UserRole = UserRoles.Moderator});
                context.Roles.Add(new Role {UserRole = UserRoles.User});

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Email = "admin@gmail.com",
                    Name = "����",
                    MobileNumber = "+375255212343",
                    Password = "123",
                    RoleId = context.Roles.First(x => x.UserRole == UserRoles.Admin).Id,
                });

                context.Users.Add(new User
                {
                    Email = "moderator@gmail.com",
                    Name = "�����",
                    MobileNumber = "+375255212883",
                    Password = "12345",
                    RoleId = context.Roles.First(x => x.UserRole == UserRoles.Moderator).Id,
                });

                context.Users.Add(new User
                {
                    Email = "tom@gmail.com",
                    Name = "�������",
                    MobileNumber = "+375255212311",
                    Password = "123456",
                    RoleId = context.Roles.First(x => x.UserRole == UserRoles.User).Id,
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