using System.Net;
using System.Net.Http.Json;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;

namespace Project.Infrastructure.Repositories;

public class ApiEmployeeRepository : IEmployeeRepository
{
    private readonly HttpClient _httpClient;

    public ApiEmployeeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Employee?> LoginAsync(string login, string password)
    {
        
        var requestData = new
        {
            Login = login,
            Password = password
        };

        
        var response = await _httpClient.PostAsJsonAsync("api/employees/login", requestData);

        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return null;
        }

        
        response.EnsureSuccessStatusCode();

        
        return await response.Content.ReadFromJsonAsync<Employee>();
    }
}