using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly AppDbContext _context;

    public OperationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Operation operation)
    {
        _context.Operations.Add(operation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate)
    {
        
        var query = _context.Operations
            .Include(o => o.Package)   
            .Include(o => o.Employee)  
            .AsQueryable();

        
        if (fromDate.HasValue)
        {
            query = query.Where(o => o.Timestamp >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            
            var endDate = toDate.Value.Date.AddDays(1);
            query = query.Where(o => o.Timestamp < endDate);
        }

        
        return await query
            .OrderByDescending(o => o.Timestamp)
            .ToListAsync();
    }
}