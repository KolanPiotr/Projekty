using Lab02;
using Lab02.Models;
using Microsoft.EntityFrameworkCore;

internal class Program {
    private static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("MyConnection");
        builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(connectionString));
        
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}