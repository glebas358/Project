using Project.Domain.Models;

namespace Project.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> LoginAsync(string login, string password);
}