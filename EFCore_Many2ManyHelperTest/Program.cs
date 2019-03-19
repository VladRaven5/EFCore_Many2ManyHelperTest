using EFCore_Many2ManyHelperTest.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore_Many2ManyHelperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MainContext context = new MainContext())
            {
                context.Database.Migrate();

                var authors = context.Authors.Include(author => author.AuthorBooks).ToList();
                var books = context.Books.Include(book => book.BookAuthors).ToList();

                context.AddManyToManyLink(authors.First(), books.First());
            }
            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
