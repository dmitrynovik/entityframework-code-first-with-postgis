using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace PostrgeTutorial
{
    class Program
    {
        static void Main()
        {
            try
            {
                using var ctx = new BloggingContext();
                var firstBlog = ctx.Blogs
                    .Include(b => b.Posts)
                    .Include(b => b.Person.City)
                    .FirstOrDefault();

                if (firstBlog == null)
                {
                    var seedBlog = new Blog { Name = "Dmitry", Url = "dmitry.livejournal.com" };
                    ctx.Blogs.Add(seedBlog);
                    ctx.SaveChanges();
                    firstBlog = ctx.Blogs.First();
                }

                if (firstBlog.Posts == null || !firstBlog.Posts.Any())
                {
                    var post = new Post {Blog = firstBlog, Content = "lorem ipsum", Title = "My first post"};
                    firstBlog.Posts = new List<Post> {post};
                    ctx.SaveChanges();
                }

                if (firstBlog.Person == null)
                {
                    firstBlog.Person = new Person
                    {
                        Name = "Dmitry Novik",
                        City = new City
                        {
                            Name = "Sutherland", Location = new Point(new Coordinate(-34.0297, 151.0593))
                        }
                    };
                    ctx.SaveChanges();
                }

                Console.WriteLine($"Found blog: {firstBlog?.Name} of {firstBlog?.Person?.Name} living in {firstBlog?.Person?.City?.Name}");

                var citiesCloseToSydney = ctx.Cities
                    .Where(c => c.Location.Distance(new Point(new Coordinate(-33.8688, 151.2093))) < 30000)
                    .Select(c => c.Name)
                    .ToArray();

                Console.WriteLine("Cities close to Sydney: {0}", arg0: string.Join(",", citiesCloseToSydney));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
