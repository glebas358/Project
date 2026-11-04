using Project.Domain.Models;
namespace Project.Api.Repositories.Interfaces
{
    public interface IPackageRepository
    {
        Task<Package?> GetByNumberAsync(string number);

        Task<List<Package>> GetByClientIdAsync(long id);


        Task UpdateAsync(Package package);

        

    }
}
