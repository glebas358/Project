using Project.Domain.Models;

namespace Project.Api.Services.Interfaces;

public interface IAuthService
{
    Task<Employee?> LoginAsync(string login, string password);
}