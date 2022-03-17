using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission11.Models;

namespace Mission11.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Basket repo;

        public CartSummaryViewComponent(Basket temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            //Displaying the cart summary
            return View(repo);
        }
    }
}
