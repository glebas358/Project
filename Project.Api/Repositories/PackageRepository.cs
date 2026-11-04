using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class PackageRepository : IPackageRepository
    {

        private readonly AppDbContext _context;

        public PackageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Package?> GetByNumberAsync(string number)
        {
            return await _context.Packages
                .Include(p => p.StorageLocation)
                .FirstOrDefaultAsync(e => e.Number == number);
        }

        public async Task<List<Package>> GetByClientIdAsync(long clientId)
        {
            return await _context.Packages
            .Include(p => p.StorageLocation) 
            .Include(p => p.Client)          
            .Where(p => p.ClientId == clientId) 
            .ToListAsync();
        }

        public async Task UpdateAsync(Package package)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync(); 
        }
    }
}
