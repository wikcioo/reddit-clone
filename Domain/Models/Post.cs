namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public User Owner { get; set; }

    private Post()
    {
    }

    public Post(string title, string content, User owner)
    {
        Title = title;
        Content = content;
        Owner = owner;
    }
}