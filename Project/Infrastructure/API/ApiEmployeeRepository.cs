using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using Project.Infrastructure.Helpers;
using System.Net.Http.Json;

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


        await response.EnsureSuccessAsync();

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (loginResponse != null)
        {
            
            AuthTokenHolder.Token = loginResponse.Token;
            return loginResponse.Employee;
        }

        return await response.Content.ReadFromJsonAsync<Employee>();


    }


        
    
}