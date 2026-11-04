using Project.Domain.Models;



namespace Project.Api.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByLoginAsync(string login);
}