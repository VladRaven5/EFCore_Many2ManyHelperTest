using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore_Many2ManyHelperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MainContext context = new MainContext())
            {
                context.Database.Migrate();
            }
            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
