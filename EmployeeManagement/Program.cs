using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using EmployeeManagement.Repositories;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add db context
            builder.Services.AddDbContext<Data.AppDbContext>(options =>
                options.UseInMemoryDatabase("EmployeeDb"));

            //add cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            //add employee repository to the dependecy injection(DI)
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //add controllers
            builder.Services.AddControllers();

            //add swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //build the app
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("MyCors");

            app.MapControllers();

            app.Run();
        }
    }
}
