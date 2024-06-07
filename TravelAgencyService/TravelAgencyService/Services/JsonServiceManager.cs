namespace TravelAgencyService.Services;
using System.Text.Json;

public class JsonServiceManager
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    private const string FilePath = "bookings.json";

    public void SaveBookingToJson(Destination destination, int qty, bool isAdd)
    {
        var bookings = LoadBookings();
        UpdateBookings(bookings, destination, qty,  isAdd);
        SaveBookingsToFile(bookings);
    }
    
    public Dictionary<string, int> LoadBookings()
    {
        if (!File.Exists(FilePath))
        {
            return new Dictionary<string, int>();
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
    }
    
    private void UpdateBookings(Dictionary<string, int> bookings, Destination destination, int qty, bool isAdd)
    {
        if (!bookings.TryGetValue(destination.Name, out var currentQty))
        {
            ValidateNewBooking(destination, qty);
            bookings[destination.Name] = qty;
        }
        else
        {
            ValidateBookingUpdate(destination, qty, currentQty, isAdd);
            bookings[destination.Name] = isAdd ? currentQty + qty : currentQty - qty;
        }
    }

    private void ValidateNewBooking(Destination destination, int qty)
    {
        if (qty > destination.MaxBookings)
        {
            throw new InvalidDestinationException("There are no available reservations for this trip. Please, try again later.");
        }
    }

    private void ValidateBookingUpdate(Destination destination, int qty, int currentQty, bool isAdd)
    {
        var newQty = isAdd ? currentQty + qty : currentQty - qty;
        if (newQty < 0 || newQty > destination.MaxBookings)
        {
            throw new InvalidDestinationException("There are no available reservations for this trip. Please, try again later.");
        }
    }

    private void SaveBookingsToFile(Dictionary<string, int> bookings)
    {
        var json = JsonSerializer.Serialize(bookings, _jsonOptions);
        File.WriteAllText(FilePath, json);
    }
}