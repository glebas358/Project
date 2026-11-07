using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore; 
using Project.Api.Controllers;
using Project.Api.Data;
using Project.Api.Repositories;
using Project.Api.Repositories.Interfaces;
using Project.Api.Services;
using Project.Api.Services.Interfaces;
using System.Text;
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

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"]!;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Ошибка аутентификации: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            builder.Services.AddHostedService<DatabaseWarmupService>();

            builder.Services.AddHostedService<DeadlineBackgroundWorker>();
            
            builder.Services.AddScoped<DeadlineService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IPackageRepository, PackageRepository>();

            builder.Services.AddScoped<IPackageService, PackageService>();

            builder.Services.AddScoped<IStorageLocationRepository, StorageLocationRepository>();

            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IReturnReasonRepository, ReturnReasonRepository>();

            builder.Services.AddScoped<IReturnRepository, ReturnRepository>();

            builder.Services.AddScoped<ITokenService, TokenService>();





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

            


            app.UseAuthentication(); // Проверяем токен (кто ты?)
            app.UseAuthorization();  // Проверяем права (можно ли тебе это?)


            app.MapControllers();





            app.Run();
        }
    }
}
