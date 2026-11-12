using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using System.Net.Http.Json;


namespace Project.Infrastructure.Repositories
{
    public class ApiReturnReasonRepository : IReturnReasonRepository
    {
        private readonly HttpClient _httpClient;

        public ApiReturnReasonRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReturnReason>> GetActiveReasonsAsync()
        {
            
            var response = await _httpClient.GetAsync("api/returnreasons");

            if (!response.IsSuccessStatusCode)
                return new List<ReturnReason>();

            return await response.Content.ReadFromJsonAsync<List<ReturnReason>>() ?? new List<ReturnReason>();
        }
    }
}
