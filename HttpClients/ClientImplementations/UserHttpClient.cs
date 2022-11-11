using System.Net.Http.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task CreateAsync(UserCreateDto dto)
    {
        var response = await _client.PostAsJsonAsync($"/users", dto);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}