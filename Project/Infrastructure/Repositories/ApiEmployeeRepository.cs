using Project.Application.Interfaces.Repositories;
using Project.Application.Services;
using Project.Domain.Models;
using System.Net;
using System.Net.Http.Json;
using static Project.Infrastructure.Repositories.ApiEmployeeRepository;

namespace Project.Infrastructure.Repositories;

public class ApiEmployeeRepository : IEmployeeRepository
{
    private readonly HttpClient _httpClient;

    public ApiEmployeeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public class LoginResponse
    {
        public Employee Employee { get; set; }
        public string Token { get; set; }
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

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (loginResponse != null)
        {
            
            AuthTokenHolder.Token = loginResponse.Token;
            return loginResponse.Employee;
        }

        return null;
    }


        
    
}