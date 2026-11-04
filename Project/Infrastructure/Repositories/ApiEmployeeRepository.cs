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
        // 1. Формируем объект для отправки на сервер. 
        // Сервер ожидает поля Login и Password.
        var requestData = new
        {
            Login = login,
            Password = password
        };

        // 2. Отправляем POST-запрос на наш сервер (Project.Api)
        // Адрес будет склеен с BaseAddress: http://[твой_IP]:5071/api/employees/login
        var response = await _httpClient.PostAsJsonAsync("api/employees/login", requestData);

        // 3. Если сервер ответил 401 (Unauthorized), значит пароль неверный
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return null;
        }

        // 4. Если сервер вернул ошибку (например, 500), выбрасываем исключение
        response.EnsureSuccessStatusCode();

        // 5. Если всё хорошо (200 OK), читаем JSON и превращаем его в объект Employee
        return await response.Content.ReadFromJsonAsync<Employee>();
    }
}