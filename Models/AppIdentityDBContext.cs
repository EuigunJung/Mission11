using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Securing the admin features of the app that we created in Mission#10. 

namespace Mission11.Models
{
    //Setting up for users to log in 
    public class AppIdentityDBContext : IdentityDbContext<IdentityUser>
    {
        //Contructor that sets up the options for this context file
        public AppIdentityDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
