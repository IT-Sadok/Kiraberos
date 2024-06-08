namespace TravelAgencyService.Services;
using System.Text.Json;

public class JsonServiceManager
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    private const string FilePath = "bookings.json";
    
    public Dictionary<string, int> LoadBookings()
    {
        if (!File.Exists(FilePath))
        {
            return new Dictionary<string, int>();
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
    }
    
    public void SaveBookingsToFile(Dictionary<string, int> bookings)
    {
        var json = JsonSerializer.Serialize(bookings, _jsonOptions);
        File.WriteAllText(FilePath, json);
    }
}