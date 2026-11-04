
using Microsoft.EntityFrameworkCore; 
using Project.Api.Controllers;
using Project.Api.Data;
using Project.Api.Repositories;
using Project.Api.Repositories.Interfaces;
using Project.Api.Services;
using Project.Api.Services.Interfaces;
namespace Project.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, 
                        maxRetryDelay: TimeSpan.FromSeconds(10), 
                        errorCodesToAdd: null)
                ));


            builder.Services.AddControllers();

            builder.Services.AddHostedService<DatabaseWarmupService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IPackageRepository, PackageRepository>();

            builder.Services.AddScoped<IPackageService, PackageService>();

            builder.Services.AddScoped<IStorageLocationRepository, StorageLocationRepository>();

            builder.Services.AddScoped<IClientRepository, ClientRepository>();




            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            


            

            app.UseAuthorization();

            app.MapControllers();





            app.Run();
        }
    }
}
