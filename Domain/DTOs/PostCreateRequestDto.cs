namespace Domain.DTOs;

public class PostCreateRequestDto
{
    public string Title { get; set; }   = string.Empty;
    public string Content { get; set; } = string.Empty;
}