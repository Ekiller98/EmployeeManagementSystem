using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using EmployeeManagement.Repositories;
{
    
}

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Data.AppDbContext>(options =>
                options.UseInMemoryDatabase("EmployeeDb"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                    builder =>
                    {
                        builder.WithOrigins("http//localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            //add employee repository to the dependecy injection(DI)
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors("MyCors");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
