using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace ConsoleApp.PostgreSQL
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseNpgsql("Host=localhost;Database=blogs;Username=admin;Password=Password1?",
                o => o.UseNetTopologySuite());

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // !
            builder.HasPostgresExtension("postgis");

            builder.Entity<Blog>().HasIndex(b => b.Name);
            builder.Entity<Blog>().HasIndex(b => b.Url);
            builder.Entity<Post>().HasIndex(b => b.Title);

            // Set column type geography:
            builder.Entity<City>().Property(c => c.Location).HasColumnType("geography (point)");
            builder.Entity<City>().HasIndex(c => c.Name);
        }
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
        public string Name { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string Title { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}