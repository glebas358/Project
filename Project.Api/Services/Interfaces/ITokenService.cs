using Project.Domain.Models;

namespace Project.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(Employee employee);
}