using LibraryApp.BAL.Interfaces;
using LibraryApp.BAL.Repositories;
using LibraryApp.DAL.Data;
using LibraryApp.DAL.Models;
using LibraryApp.DAL.Seeded_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.PAL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });
            builder.Services.AddScoped<ICrudOperations<Book>, CrudOperations<Book>>();
            builder.Services.AddScoped<ICrudOperations<Borrower>, CrudOperations<Borrower>>();
            builder.Services.AddScoped<ICrudOperations<BorrowingOP>, CrudOperations<BorrowingOP>>();

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<LibraryContext>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
            });

            var app = builder.Build();
            using var scope =app.Services.CreateScope();
            var services=scope.ServiceProvider;
            var looggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = looggerFactory.CreateLogger("app");
            try
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await DefaultUser.SeedUser(userManager,roleManager);
                logger.LogInformation("Data seeded");
                logger.LogInformation("App started");

            }catch(Exception ex)
            {
                logger.LogWarning(ex, "an error ecured while seeding data");
            }

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}