using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByLoginAsync(string login)
    {
        
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Login == login);
    }
}