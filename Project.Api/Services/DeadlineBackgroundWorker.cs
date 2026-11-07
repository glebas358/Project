using Project.Api.Data;
using Microsoft.Extensions.DependencyInjection;
namespace Project.Api.Services
{
    public class DeadlineBackgroundWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DeadlineBackgroundWorker> _logger;

        public DeadlineBackgroundWorker(IServiceScopeFactory scopeFactory, ILogger<DeadlineBackgroundWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Фоновая проверка сроков запущена.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    
                    using var scope = _scopeFactory.CreateScope();
                    var deadlineService = scope.ServiceProvider.GetRequiredService<DeadlineService>();

                    
                    await deadlineService.CheckExpiredPackagesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Ошибка в фоновой проверке: {ex.Message}");
                }

                
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }

        }
    }
}
