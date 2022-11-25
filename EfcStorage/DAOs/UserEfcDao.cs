using Application.DaoInterfaces;
using Domain.Models;

namespace EfcStorage.DAOs;

public class UserEfcDao : IUserDao
{
    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}