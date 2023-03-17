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

        // TODO с этим надо что-то сделать
        //builder.Services.AddTransient<Datamanager>();

        var connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'JournalContextConnection' not found.");

        builder.Services.AddDbContext<JournalContext>(options =>{
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });


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

builder.Services.AddRazorPages();
        // builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        // .AddCookie(options => options.LoginPath = "/login");


        builder.Services.AddIdentity<Emp, IdentityRole>(/*opts =>
            {
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }*/).AddEntityFrameworkStores<JournalContext>()
            .AddDefaultTokenProviders().AddDefaultUI();

            //настраиваем authentication cookie
            // builder.Services.ConfigureApplicationCookie(options =>
            // {
            //     options.Cookie.Name = "JournalAuth";
            //     options.Cookie.HttpOnly = true;
            //     options.LoginPath = "/account/login";
            //     options.AccessDeniedPath = "/account/accessdenied";
            //     options.SlidingExpiration = true;
            // });

            //настраиваем политику авторизации для Admin area
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

      //  app.MapRazorPages();
        app.Run();
    }
}
