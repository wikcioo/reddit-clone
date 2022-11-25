using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
}