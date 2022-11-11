using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicImplementations;

public class PostLogic : IPostLogic
{
    private readonly IUserDao _userDao;
    private readonly IPostDao _postDao;

    public PostLogic(IUserDao userDao, IPostDao postDao)
    {
        _userDao = userDao;
        _postDao = postDao;
    }
    
    public async Task<Post> CreateAsync(PostCreateDto dto)
    {
        var user = await _userDao.GetByUsernameAsync(dto.Username);
        if (user == null)
        {
            throw new Exception($"'{dto.Username}' user was not found!");
        }

        var post = new Post(dto.Title, dto.Content, user);
        ValidatePost(post);
        return await _postDao.CreateAsync(post);
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        var post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id '{id}' was not found!");
        }

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _postDao.GetAllAsync();
    }

    private static void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.Title))   throw new Exception("Post's title cannot be empty!");
        if (string.IsNullOrEmpty(post.Content)) throw new Exception("Post's content cannot be empty!");
    }
}