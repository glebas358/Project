using Project.Domain.Models;
namespace Project.Api.Repositories.Interfaces
{
    public interface IStorageLocationRepository
    {
        Task<StorageLocation?> GetByZoneAndCellAsync(string zone, string cell);

        Task<StorageLocation?> GetByIdAsync(long id);

        Task UpdateAsync(StorageLocation storageLocation);
    }
}
