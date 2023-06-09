using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using journalapp.Service;
using journalapp;
namespace journalapp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'JournalContextConnection' not found.");

        builder.Services.AddDbContext<JournalContext>(options =>{
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        }).AddTransient<JournalContext>();


        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession(options =>
        {
        options.IdleTimeout=TimeSpan.FromMinutes(45);
        options.Cookie.HttpOnly=true;
        options.Cookie.IsEssential=true;
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews(x =>
                {
                    x.Conventions.Add(new AdminAreaAutorization("Admin", "AdminArea"));
                });

        builder.Services.AddRazorPages(options =>
            options.Conventions.AuthorizePage("/Register", WC.AdminRole));


        builder.Services.AddIdentity<Emp, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
                opts.Lockout.AllowedForNewUsers = false;
            }).AddEntityFrameworkStores<JournalContext>()
                .AddDefaultTokenProviders().AddDefaultUI();

            // настраиваем политику авторизации для Admin area
        builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole(WC.AdminRole); });
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

        app.UseStatusCodePages();

        app.UseRouting();

        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSession();
        // маршрутизация
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name:"admin", 
                pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Run();
    }
}
