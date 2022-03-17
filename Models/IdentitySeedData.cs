using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// This file is seeding the identification data to the Identity.sqlite
namespace Mission11.Models
{
    //"static" meaning applying chanes to the class itself
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "413ExtraYeetPeriod(t)!";

        //Making sure the initial data is present 
        public static async void EnsurePopulated (IApplicationBuilder app)
        {
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();


        //Run the migration manager of there is db to be migrated
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

        //Is there a ID called "adminUser"? If there is no match, create a new adminUser with such information:
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "euigunj@gmail.com";
                user.PhoneNumber = "385-286-9492";

                await userManager.CreateAsync(user, adminPassword);
            }

            

        
        }
    }
}

// Seeding data failed: Mission 11 05 Login Form "00:49" - the AspNetUsers do not get any updated ID