using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        //Adding items to the basket
        //virtual allows this method to be overwritten when we inherit from it
        public virtual void AddItem (Book book, int qty)
        {
            //Get the lineitem that matches the bookid
            BasketLineItem line = Items
                .Where(p => p.Book.BookID == book.BookID)
                .FirstOrDefault();

            //condition: if the line is null create a new instance of Basketlineitem with the information
            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        //Remove selected Item from the cart 
        public virtual void RemoveItem (Book book)
        {
            Items.RemoveAll(x => x.Book.BookID == book.BookID);
        }

        //Remove everything from the cart 
        public virtual void ClearBasket()
        {
            Items.Clear();
        }

        public double CalculateTotal()
        {
                double sum = Items.Sum(x => x.Quantity * x.Book.Price);
                return sum;
        }

        public class BasketLineItem
        {
            [Key]
            public int LineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }


        }
    }
}
