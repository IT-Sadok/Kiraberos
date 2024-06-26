using TravelAgencyService.Interfaces;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

public class AgencyModel(IBookingHandler bookingHandler, Dictionary<string, BookingInfo> allBookings)
{
    private readonly List<Destination?> _destinations =
    [
        new Destination("Spain", 5),
        new Destination("Ukraine", 3),
        new Destination("Japan", 4),
        new Destination("Norway", 2),
        new Destination("New Zealand", 3)
    ];

    public void BookTicket(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);

            if (destination == null)
            {
                throw new InvalidDestinationException("Wrong destination. Please, choose the right one.");
            }
            bookingHandler.BeforeSaveBookingToJson(destination, qty, true, allBookings); 
    }

    public bool CancelBooking(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);
        if (destination == null)
        {
            return false;
        }
        bookingHandler.BeforeSaveBookingToJson(destination, qty, false, allBookings); 
        return true;
    }
    
    private Destination? FindDestination(string destinationName)
    {
        return _destinations.FirstOrDefault(d => d != null && d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }
    
       public Dictionary<string, BookingInfo> GetAllBookings()
    {
        return allBookings;
    }
    
    public Dictionary<string, BookingInfo> GetAllBookingsByDate(DateTime date)
    {
        return GetAllBookings()
            .Where(booking => booking.Value.BookingDate.Date == date.Date)
            .ToDictionary(booking => booking.Key, booking => booking.Value);
    }
}