using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.ClientImplementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task CreateAsync(PostCreateDto dto)
    {
        var response = await _client.PostAsJsonAsync("/posts", dto);
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"/posts/{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var response = await _client.GetAsync($"/posts");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;

        return posts;
    }
}