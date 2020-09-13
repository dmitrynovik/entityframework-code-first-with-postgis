using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.PostgreSQL;

namespace PostrgeTutorial
{
    class Program
    {
        static void Main()
        {
            try
            {
                using var ctx = new BloggingContext();
                var firstBlog = ctx.Blogs.FirstOrDefault();
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

                Console.WriteLine($"Found blog: {firstBlog?.Name}");
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
