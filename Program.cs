using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace journalapp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'JournalContextConnection' not found.");

         builder.Services.AddDbContext<JournalContext>(options => options.UseSqlServer(connectionString));

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

        // builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        // .AddCookie(options => options.LoginPath = "/login");

        builder.Services.AddAuthorization();

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
app.UseMvcWithDefaultRoute();

        app.UseRouting();

app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
        app.Run();
    }
}
