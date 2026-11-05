using Microsoft.EntityFrameworkCore;
using Project.Api.Data;

namespace Project.Api.Services;

public class DatabaseWarmupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DatabaseWarmupService> _logger;

    public DatabaseWarmupService(IServiceScopeFactory scopeFactory, ILogger<DatabaseWarmupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Начинаем прогрев базы данных Supabase...");

        
        for (int i = 0; i < 3; i++)
        {
            try
            {
            
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

               
                await context.Database.ExecuteSqlRawAsync("SELECT 1", stoppingToken);

                _logger.LogInformation("База данных успешно прогрета и готова к работе!");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Попытка прогрева {i + 1} не удалась: {ex.Message}");
                await Task.Delay(5000, stoppingToken); 
            }
        }

        _logger.LogError("Не удалось прогреть базу. Первый запрос может быть медленным.");
    }
}