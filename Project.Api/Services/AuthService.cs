using Project.Api.Repositories.Interfaces;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project.Api.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;

    public AuthService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee?> LoginAsync(string login, string password)
    {
        
        var employee = await _employeeRepository.GetByLoginAsync(login);

        if (employee == null) return null;

        
        var passwordHash = HashPassword(password);
        if (employee.PasswordHash != passwordHash) return null;

        
        return employee;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}