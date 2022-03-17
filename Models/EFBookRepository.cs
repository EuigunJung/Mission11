using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11.Models
{
    public class EFBookRepository : IBookRepository
    {
        public BookContext context {get; set;}
        public EFBookRepository (BookContext temp)
        {
            context = temp;
        }

       //Continue adding the CRUD features from (IBookRepository)
        public IQueryable<Book> Books => context.Books;

        public void SaveBook(Book b)
        {
            context.SaveChanges();
        }

        public void CreateBook(Book b)
        {
            context.Add(b);
            context.SaveChanges();
        }

        public void DeleteBook(Book b)
        {
            context.Remove(b);
            context.SaveChanges();
        }
    }
}
