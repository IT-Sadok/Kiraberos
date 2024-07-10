using System.Text.Json;

namespace StudentManager.Utilities;

public class FileManager
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    private static SemaphoreSlim _semaphoreSlim = new(1, 1);
    
    public async Task SaveDataToFileAsync<T>(T data, string filePath)
    {
        await _semaphoreSlim.WaitAsync();
        try
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}