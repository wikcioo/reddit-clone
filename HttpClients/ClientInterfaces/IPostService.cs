using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreateDto dto);
    Task<Post> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllAsync();
}