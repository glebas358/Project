
using Project.Application.Interfaces.Repositories;
using Project.Domain.Enums;
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;



namespace Project.Infrastructure.Repositories
{
    public class ApiPackageRepository : IPackageRepository
    {
        private readonly HttpClient _httpClient;

        public ApiPackageRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Package?> RegisterPackageAsync(string number, string zone, string cell)
        {
            var requestData = new
            { Number = number, 
              Zone = zone, 
              Cell = cell 
            };


            var response = await _httpClient.PostAsJsonAsync("api/packages/register", requestData);

            if (!response.IsSuccessStatusCode)
            {
                
                var errorResponse = await response.Content.ReadFromJsonAsync<ServerError>();
                
                throw new Exception(errorResponse?.Message ?? "Неизвестная ошибка сервера");
            }


            return await response.Content.ReadFromJsonAsync<Package>();
        }
        public async Task<Package?> SearchByNumberAsync(string number)
        {
            var requestData = new
            {
                Number = number
            };

            var response = await _httpClient.PostAsJsonAsync("api/packages/searchByNumber/{number}", requestData);

            if (!response.IsSuccessStatusCode)
            {
                
                var errorResponse = await response.Content.ReadFromJsonAsync<ServerError>();
                
                throw new Exception(errorResponse?.Message ?? "Неизвестная ошибка сервера");
            }

            return await response.Content.ReadFromJsonAsync<Package>();



        }

        public async Task<List<Package>> SearchByPhoneAsync(string phone)
        {
            var requestData = new { Phone = phone };

            var response = await _httpClient.PostAsJsonAsync("api/packages/searchByPhone/{ phone}", requestData);

            if (!response.IsSuccessStatusCode)
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ServerError>();

                throw new Exception(errorResponse?.Message ?? "Неизвестная ошибка сервера");
            }

            return await response.Content.ReadFromJsonAsync<List<Package>>();
        }

        public async Task<List<Package>> SearchByNameAsync(string name)
        {
            var requestData = new { Name = name };

            var response = await _httpClient.PostAsJsonAsync("api/packages/searchByName/{name}", requestData);

            if (!response.IsSuccessStatusCode)
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ServerError>();

                throw new Exception(errorResponse?.Message ?? "Неизвестная ошибка сервера");
            }

            return await response.Content.ReadFromJsonAsync<List<Package>>();

        }

        public async Task<Package> ExtraditionPackageAsync(string number)
        {
            var requestData = new { Number = number };

            var response = await _httpClient.PostAsJsonAsync("api/packages/extradition", requestData);

            if (!response.IsSuccessStatusCode)
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ServerError>();

                throw new Exception(errorResponse?.Message ?? "Неизвестная ошибка сервера");
            }

            return await response.Content.ReadFromJsonAsync<Package>();
        }



        public class ServerError
        {
            public string Message { get; set; } = string.Empty;
        }


        public async Task<List<Package>> GetByStatusAsync(PackageStatus status)
        {
            return null;
        }

        public async Task UpdateAsync(Package package)
        {

        }
    }
}
