using System.Net.Http.Json;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using Project.Infrastructure.Helpers; 

namespace Project.Infrastructure.Repositories;

public class ApiOperationRepository : IOperationRepository
{
    private readonly HttpClient _httpClient;

    public ApiOperationRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Operation>> GetHistoryAsync(DateTime? fromDate, DateTime? toDate)
    {
        
        string url = "api/operations?";

        
        if (fromDate.HasValue)
        {
            var fromUtc = fromDate.Value.Date.ToUniversalTime();
            url += $"fromDate={fromUtc:yyyy-MM-ddTHH:mm:ssZ}&";
        }

        if (toDate.HasValue)
        {
            
            var toUtc = toDate.Value.Date.AddDays(1).AddSeconds(-1).ToUniversalTime();
            url += $"toDate={toUtc:yyyy-MM-ddTHH:mm:ssZ}";
        }

        
        var response = await _httpClient.GetAsync(url);

        
        await response.EnsureSuccessAsync();

        
        return await response.Content.ReadFromJsonAsync<List<Operation>>() ?? new List<Operation>();
    }
}