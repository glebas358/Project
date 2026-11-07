using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly AppDbContext _context;

        public ReturnRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Return returnRecord)
        {
            
            _context.Returns.Add(returnRecord);

            
            await _context.SaveChangesAsync();
        }
    }
}
