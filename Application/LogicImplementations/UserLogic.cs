using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicImplementations;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreateDto dto)
    {
        var existing = await _userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
        {
            throw new Exception($"'{dto.Username}' username already exists!");
        }

        var newUser = new User()
        {
            Username = dto.Username,
            Password = dto.Password
        };

        return await _userDao.CreateAsync(newUser);
    }
}