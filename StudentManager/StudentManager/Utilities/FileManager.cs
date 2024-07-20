using System.Text.Json;

namespace StudentManager.Utilities;

public class FileManager
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    
    public async Task SaveDataToFileAsync<T>(T data, string filePath)
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }
}