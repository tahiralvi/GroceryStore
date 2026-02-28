using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using GroceryStoreAPI.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GroceryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            // Registration of the new Order Service
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IItemService, ItemService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Required for Bootstrap/CSS in _Layout.cshtml

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Default route for MVC (Home page)
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Keep support for existing API attributes
                endpoints.MapControllers();
            });
        }
    }
}