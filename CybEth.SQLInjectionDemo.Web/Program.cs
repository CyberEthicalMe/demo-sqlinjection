
namespace CybEth.SQLInjectionDemo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            //builder.Services.AddDbContext
            //Scaffold-DbContext -Name ConnectionStrings:Contoso Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
            //dotnet ef dbcontext scaffold Name=ConnectionStrings:Contoso Microsoft.EntityFrameworkCore.SqlServer
            //dotnet user-secrets set ConnectionStrings:Contoso "Server=192.168.56.5;Database=ContosoDB_Test;User Id=dbaccess;Password=P@ssw0rd;Encrypt=false;"

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}