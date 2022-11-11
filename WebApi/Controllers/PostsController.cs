using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostLogic _postLogic;

    public PostsController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Post>> CreateAsync(PostCreateRequestDto dto)
    {
        try
        {
            if (User.Identity == null)
            {
                return Unauthorized("Not logged in!");
            }
            
            var post = await _postLogic.CreateAsync(new PostCreateDto()
            {
                Username = User.Identity.Name!,
                Title    = dto.Title,
                Content  = dto.Content
            });
            
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var post = await _postLogic.GetByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            var posts = await _postLogic.GetAllAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}