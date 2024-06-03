using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ASM2_KSTH.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASM2_KSTH.Helpers;
namespace ASM2_KSTH
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ASM2_KSTHContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ASM2_KSTHContext") ?? throw new InvalidOperationException("Connection string 'ASM2_KSTHContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.LoginPath = "/Admins/Index";
                options.AccessDeniedPath = "/AccessDenied";
            });
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admins}/{action=Index}/{id?}");

            app.Run();
        }
    }
}