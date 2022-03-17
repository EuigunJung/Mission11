using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11.Models
{
    //interface is used instead of class (this is an instance that forces it to use this template) 
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }

        //Adding the CRUD methods for the Books
        public void SaveBook(Book b);
        public void CreateBook(Book b);
        public void DeleteBook(Book b);



    }
}
