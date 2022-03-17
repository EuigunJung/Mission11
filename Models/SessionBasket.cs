using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission11.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission11.Models
{
    //Inheriting the Basket class 
    public class SessionBasket : Basket
    {
       //returning an instance of Basket

        public static Basket GetBasket (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Creating an instance of a basket -- pull the information from either new or old sessionbasket
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            basket.Session = session;
            
            return basket;
        }

        //JsonIgnore prevents a property being serialized 
        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            // this particular "instance of the object"
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }

    }
}
