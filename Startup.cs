using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //service that tells asp.net to use the MVC pattern. 
            services.AddControllersWithViews();

            services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });
            //Adding the identity DB, connecting with the appsettings.json
            services.AddDbContext<AppIdentityDBContext>(options => 
                options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<IBookRepository, EFBookRepository>();
            services.AddScoped<ICheckoutRepository, EFCheckoutRepository>();
            //Enabling Razor Pages
            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            //when we see basket, get a new basket method to put the info for a particular session. 
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>() ;

            //adding the blazor serverside for configuration 
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //tell asp.net to use the static files in the wwwroot folder
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            //Identification feature before reaching the endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //*Endpoint is executed in order. 
                endpoints.MapControllerRoute("categorypage", "{bookCategory}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //setting endpoint to display the pageNum only in the pattern parameter:

                endpoints.MapControllerRoute(
                name: "Paging",
                pattern: "Page{pageNum}",
                defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category", "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


                //to follow controller -> action -> ID
                //This is different than other default settings, something like: pattern: "{controller:..}{Route:...}{id=?}"
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                // this lets to this path when url is not found
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
