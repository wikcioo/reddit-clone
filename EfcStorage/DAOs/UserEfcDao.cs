using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcStorage.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly EfcContext _context;

    public UserEfcDao(EfcContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        var newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(
            user => user.Username.ToLower().Equals(username.ToLower())
        );

        return existing;
    }
}