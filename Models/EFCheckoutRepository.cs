using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookContext context;

        public EFCheckoutRepository(BookContext temp)
        {
            context = temp;
        }
        public IQueryable<Checkout> Checkouts => context.Checkouts.Include(x => x.Lines).ThenInclude(x => x.Book);


        public void SaveCheckout(Checkout checkout)
        {
            context.AttachRange(checkout.Lines.Select(x => x.Book));

            if (checkout.PurchaseId == 0)
            {
                context.Checkouts.Add(checkout);
            }

            context.SaveChanges();
        }
    }
}
