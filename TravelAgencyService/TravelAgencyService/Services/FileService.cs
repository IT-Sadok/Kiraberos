using System.Text.Json;
using TravelAgencyService.Models;

namespace TravelAgencyService.Services;

public class FileService
{
    private const string FilePath = "bookings.json";
    private readonly FileManager<Dictionary<string, BookingInfo>> _fileManager;
    public readonly Dictionary<string, BookingInfo> AllBookings;


    public FileService()
    {
        _fileManager = new FileManager<Dictionary<string, BookingInfo>>(FilePath);
        AllBookings = LoadBookings();
    }

    private Dictionary<string, BookingInfo> LoadBookings()
    {
        return _fileManager.LoadBookingsFromFile() ?? new Dictionary<string, BookingInfo>();
    }
    
    public void SaveBookingsToFile(Dictionary<string, BookingInfo> bookings)
    {
        _fileManager.SaveToFile(bookings);
    }
}