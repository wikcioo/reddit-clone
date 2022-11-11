using System.Text.Json;
using Domain.Models;

namespace FileStorage;

public class FileContext
{
    private const string FilePath = "data.json";

    private DataContainer? _dataContainer;

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users!;
        }
    }

    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return _dataContainer!.Posts!;
        }
    }

    private void LoadData()
    {
        if (_dataContainer != null) return;
        if (!File.Exists(FilePath))
        {
            _dataContainer = new DataContainer()
            {
                Users = new List<User>(),
                Posts = new List<Post>()
            };

            return;
        }

        var content = File.ReadAllBytes(FilePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        var serialized = JsonSerializer.Serialize(_dataContainer, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    }
}