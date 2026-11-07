using System;
using System.Text.Json;

namespace Project.Infrastructure.Helpers;

public static class HttpResponseExtensions
{
    
    public static async Task EnsureSuccessAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return; 

       
        var errorText = await response.Content.ReadAsStringAsync();

        
        string message = errorText;
        try
        {
            using var doc = JsonDocument.Parse(errorText);
            if (doc.RootElement.TryGetProperty("message", out var messageProp))
            {
                message = messageProp.GetString() ?? errorText;
            }
            else if (doc.RootElement.TryGetProperty("errors", out var errorsProp))
            {
                
                message = $"Ошибка валидации: {errorsProp}";
            }
        }
        catch (JsonException)
        {
            
        }

        
        throw new Exception($"{(int)response.StatusCode} ({response.StatusCode}): {message}");
    }
}