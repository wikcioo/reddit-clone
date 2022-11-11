using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.LogicImplementations;

public class AuthLogic : IAuthLogic
{
    private readonly IUserDao _userDao;

    public AuthLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        var existingUser = await _userDao.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception($"'{username}' user not found!");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Incorrect password!");
        }

        return existingUser;
    }
}