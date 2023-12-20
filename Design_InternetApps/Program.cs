using Design_InternetApps.Models;
using Microsoft.EntityFrameworkCore;

namespace Design_InternetApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("EmployeeList"));
            builder.Services.AddControllers();
            
            var app = builder.Build();
            
            app.MapControllers();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}