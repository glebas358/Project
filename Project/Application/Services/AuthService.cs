using Project.Application.Interfaces.Repositories;
using Project.Application.Interfaces.Services;
using Project.Domain.Models;

namespace Project.Application.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;

    public AuthService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee?> LoginAsync(string login, string password)
    {
        
        return await _employeeRepository.LoginAsync(login, password);
    }
}