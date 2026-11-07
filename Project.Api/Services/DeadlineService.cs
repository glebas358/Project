using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Enums;

namespace Project.Api.Services
{
    public class DeadlineService
    {
  
        private readonly ILogger<DeadlineService> _logger;

        private readonly IPackageRepository _packageRepository;


        public DeadlineService(IPackageRepository packageRepository, ILogger<DeadlineService> logger)
        {
            _logger = logger;
            _packageRepository = packageRepository;
        }

        public async Task CheckExpiredPackagesAsync()
        {
            _logger.LogInformation("Проверка просроченных посылок...");

            
            var expiredPackages = await _packageRepository.GetExpiredPackageAsync();

            if (!expiredPackages.Any()) return;

            
            foreach (var package in expiredPackages)
            {
                package.Status = PackageStatus.Expired;
                _logger.LogInformation($"Посылка {package.Number} просрочена.");
                await _packageRepository.UpdateAsync(package);
            }

            
            

            _logger.LogInformation($"Обновлено просроченных посылок: {expiredPackages.Count}");
        }

    }
}
