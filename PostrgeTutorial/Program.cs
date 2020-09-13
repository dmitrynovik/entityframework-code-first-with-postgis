using System;
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
                var ctx = new BloggingContext();
                var firstBlog = ctx.Blogs.FirstOrDefault();
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
