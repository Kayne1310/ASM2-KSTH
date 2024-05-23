using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ASM2_KSTH.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                options.LoginPath = "/Students/Index";
                options.LoginPath = "/Teachers/Index";
                options.LoginPath = "/Admins/Index";
                options.AccessDeniedPath = "/";
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Students}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
