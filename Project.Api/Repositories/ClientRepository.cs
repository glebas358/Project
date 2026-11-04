using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Client?> GetByPhoneAsync(string phone)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(e => e.Phone == phone);
        }

        public async Task<Client?> GetByClientName(string name)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}
