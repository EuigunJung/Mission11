using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace Mission11.Models
{
    public class BookContext : DbContext
    {
        public BookContext()
        {
        }

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public  DbSet<Book> Books { get; set; }
        public  DbSet<Checkout> Checkouts { get; set; }
      
    }
}
