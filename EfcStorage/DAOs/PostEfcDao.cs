using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcStorage.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly EfcContext _context;

    public PostEfcDao(EfcContext context)
    {
        _context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        var newPost = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        var existing = await _context.Posts
            .AsNoTracking()
            .Include(post => post.Owner)
            .SingleOrDefaultAsync(post => post.Id == id);
        
        return existing;
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult(_context.Posts.Include(post => post.Owner).AsEnumerable());
    }
}