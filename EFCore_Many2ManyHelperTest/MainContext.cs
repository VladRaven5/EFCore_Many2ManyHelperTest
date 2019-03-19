using EFCore_Many2ManyHelperTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Many2ManyHelperTest
{
    public class MainContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connection-string");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BookAuthor>()
                .HasKey(bookAuthor => new { bookAuthor.BookId, bookAuthor.AuthorId });

            modelBuilder
                .Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Author)
                .WithMany(author => author.AuthorBooks)
                .HasForeignKey(bookAuthor => bookAuthor.AuthorId);

            modelBuilder
                .Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Book)
                .WithMany(book => book.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.BookId);

            modelBuilder
                .Entity<Book>()
                .HasData(
                new Book
                {
                    Id = 1,
                    Title = "War and Peace"                    
                },
                new Book
                {
                    Id = 2,
                    Title = "Crime and Punishment"
                });

            modelBuilder
                .Entity<Author>()
                .HasData(
                new Author
                {
                    Id = 1,
                    Name = "F M Dostoevsky",
                },
                new Author
                {
                    Id = 2,
                    Name = "L N Tolstoy"
                });

            modelBuilder
                .Entity<BookAuthor>()
                .HasData(
                new BookAuthor
                {
                    BookId = 1,
                    AuthorId = 2
                },
                new BookAuthor
                {
                    BookId = 2,
                    AuthorId = 1
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}