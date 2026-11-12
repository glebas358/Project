using System.Net.Http.Json;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using Project.Infrastructure.Helpers;

namespace Project.Infrastructure.Repositories;

public class ApiManagerNoteRepository : IManagerNoteRepository
{
    private readonly HttpClient _httpClient;

    public ApiManagerNoteRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ManagerNote>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/notes");
        await response.EnsureSuccessAsync();
        return await response.Content.ReadFromJsonAsync<List<ManagerNote>>() ?? new List<ManagerNote>();
    }

    public async Task AddAsync(string text)
    {
        var response = await _httpClient.PostAsJsonAsync("api/notes", new { Text = text });
        await response.EnsureSuccessAsync();
    }

    public async Task UpdateAsync(long id, string newText)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/notes/{id}", new { Text = newText });
        await response.EnsureSuccessAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"api/notes/{id}");
        await response.EnsureSuccessAsync();
    }
}