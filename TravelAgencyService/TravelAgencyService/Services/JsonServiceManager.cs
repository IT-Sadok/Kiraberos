using TravelAgencyService.Models;

namespace TravelAgencyService.Services;
using System.Text.Json;

public class JsonServiceManager
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    private const string FilePath = "bookings.json";
    
    public Dictionary<string, BookingInfo> LoadBookings()
    {
        if (!File.Exists(FilePath))
        {
            return new Dictionary<string, BookingInfo>();
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Dictionary<string, BookingInfo>>(json) ?? new Dictionary<string, BookingInfo>();
    }
    
    public void SaveBookingsToFile(Dictionary<string, BookingInfo> bookings)
    {
        var json = JsonSerializer.Serialize(bookings, _jsonOptions);
        File.WriteAllText(FilePath, json);
    }
}