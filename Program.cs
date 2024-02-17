using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MagyargombfociStore;

namespace MagyargombfociStore
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<MagyargombfociStoreContext>(options =>
            {

                options.UseSqlServer(connectionString);
            });



            

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MagyargombfociStoreContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IHomeRepository, HomeRepository>();
            builder.Services.AddTransient<ICartRepository, CartRepository>();
            builder.Services.AddTransient<IUserOrderRepository, UserOrderRepository>();


            var app = builder.Build();
            //using (var scope = app.Services.CreateScope())
            //{

            //    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
            //}



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");




            app.MapRazorPages();
            app.Run();
            return Task.CompletedTask;
        }
    }
}