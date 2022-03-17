using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission11.Infrastructure;
using Mission11.Models;

namespace Mission11.Pages
{
    public class ShopModel : PageModel
    {
        private IBookRepository repo { get; set; }

        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public ShopModel(IBookRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }


        public void OnGet(string returnUrl)
        {
            //this enables the user to go back to the main index page
            ReturnUrl = returnUrl ?? "/";
        }


        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // grab a specific book data matching to the id 
            Book b = repo.Books.FirstOrDefault(x => x.BookID == bookId);

            basket.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookID == bookId).Book);
            return RedirectToPage (new { ReturnUrl = returnUrl });
        }
    }
}
