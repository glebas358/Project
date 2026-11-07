using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class ReturnReasonRepository : IReturnReasonRepository
    {
        private readonly AppDbContext _context;

        public ReturnReasonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReturnReason>> GetAllActiveAsync()
        {
            return await _context.ReturnReasons
                .Where(e => e.IsActive == true)
                .ToListAsync();
        }

        public async Task<ReturnReason?> GetById(long id)
        {
            return await _context.ReturnReasons
                .FirstOrDefaultAsync(e => e.Id == id);

        }
    }
}
