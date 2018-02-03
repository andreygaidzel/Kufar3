using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kufar3.Models
{
    public class KufarContext : DbContext
    {
        public KufarContext() : base("Kufar3")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Declaration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Moderation { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual List<Image> Images { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Declaration))]
        public int DeclarationId { get; set; }
        public virtual Declaration Declaration { get; set; }
    }
}