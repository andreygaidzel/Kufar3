using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Models
{
    public class KufarContext : DbContext
    {
        public KufarContext() : base("Kufar3")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }

    public class EntityBase
    {
        public EntityBase()
        {
            CreatedDate = DateTime.UtcNow;    
        }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public UserRoles Role { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Category : EntityBase
    {
        public string Name { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory : EntityBase
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Region : EntityBase
    {
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; }
    }

    public class City : EntityBase
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Region))]
        public long RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual List<Declaration> Declarations { get; set; }
    }

    public class Declaration : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DeclarationTypes Type { get; set; }
        public string Price { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public long SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        [ForeignKey(nameof(City))]
        public long CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Image> Images { get; set; }
    }

    public class Image : EntityBase
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Declaration))]
        public long DeclarationId { get; set; }
        public virtual Declaration Declaration { get; set; }
    }
}