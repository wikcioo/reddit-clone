using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    public Task<User> CreateAsync(User user);
    public Task<User?> GetByUsernameAsync(string username);
}