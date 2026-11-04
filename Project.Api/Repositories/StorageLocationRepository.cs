using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class StorageLocationRepository : IStorageLocationRepository
    {
        private readonly AppDbContext _context;

        public StorageLocationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<StorageLocation?> GetByZoneAndCellAsync(string zone, string cell)
        {
            return await _context.StorageLocations
                .FirstOrDefaultAsync(e => e.Zone == zone && e.Cell == cell);
                
        }

        public async Task<StorageLocation?> GetByIdAsync(long id)
        {
            return await _context.StorageLocations
                .FirstOrDefaultAsync (e => e.Id == id);
        }

        public async Task UpdateAsync(StorageLocation storageLocation)
        {
            _context.StorageLocations.Update(storageLocation);
            await _context.SaveChangesAsync();
        }
    }
}
