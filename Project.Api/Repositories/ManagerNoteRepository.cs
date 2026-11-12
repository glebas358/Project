using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories
{
    public class ManagerNoteRepository : IManagerNoteRepository
    {
        private readonly AppDbContext _context;

        public ManagerNoteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ManagerNote note)
        {
            _context.ManagerNotes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ManagerNote>> GetAllAsync()
        {
            return await _context.ManagerNotes
                .Include(n => n.Employee)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(ManagerNote note)
        {
            _context.ManagerNotes.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ManagerNote note)
        {
            _context.ManagerNotes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<ManagerNote?> GetByIdAsync(long id)
        {
            return await _context.ManagerNotes
                .FirstOrDefaultAsync(e => e.Id == id);
                
        }
    }
}
