using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Mission11.Models.Basket;

namespace Mission11.Models
{
    public class Checkout
    {
        [Key]
        //It is not going to be passed to the URL slug - securing the information 
        [BindNever]
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please enter your first name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int ZIP { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the Country")]
        public string Country { get; set; }

        //Only Administrative part - Users cannot see this.
        [BindNever]
        public bool CheckoutReceived { get; set; }
    }
}
