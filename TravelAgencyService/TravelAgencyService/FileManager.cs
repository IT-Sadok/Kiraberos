using System.Text.Json;
namespace TravelAgencyService;

public class FileManager<T>
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public FileManager(string filePath)
    {
        _filePath = filePath;
    }
    
    public T? LoadBookingsFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return default(T);
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<T>(json) ?? default(T);
    }
    
    public void SaveToFile(T data)
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}