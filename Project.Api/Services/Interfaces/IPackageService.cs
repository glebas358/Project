using Project.Domain.Models;
namespace Project.Api.Services.Interfaces
{
    public interface IPackageService
    {
        Task<Package> RegisterPackageAsync(string number, string zone, string cell);

        Task<Package?> SearchPackageByNumberAsync(string number);

        Task<List<Package>> SearchPackagesByPhone(string number);

        Task<List<Package>> SearchPackagesByName(string name);

        Task<Package?> ExtraditionPackageAsync(string number);




    }
}
