using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

public class AgencyModel
{
    private readonly List<Destination?> _destinations =
    [
        new Destination("Spain", 5),
        new Destination("Ukraine", 3),
        new Destination("Japan", 4),
        new Destination("Norway", 2),
        new Destination("New Zealand", 3)
    ];

    private readonly JsonServiceManager _jsonServiceManager = new();

    public void BookTicket(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);

            if (destination == null)
            {
                throw new InvalidDestinationException("Wrong destination. Please, choose the right one.");
            }
            BeforeSaveBooking(destination, qty, true); 
    }

    public bool CancelBooking(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);
        if (destination == null)
        {
            return false;
        }
        BeforeSaveBooking(destination, qty, false);
        return true;
    }

    public Dictionary<string, int> GetAllBookings()
    {
        return _jsonServiceManager.LoadBookings();
    }
    
    private void BeforeSaveBooking(Destination destination, int qty, bool isAdd)
    {
        var bookings = GetAllBookings();
        ValidateBookings(bookings, destination, qty,  isAdd);
        _jsonServiceManager.SaveBookingsToFile(bookings);
    }

    private void ValidateBookings(Dictionary<string, int> bookings, Destination destination, int qty, bool isAdd)
    {
        var currentQty = bookings.GetValueOrDefault(destination.Name, 0);
        var newQty = isAdd ? currentQty + qty : currentQty - qty;
        if (newQty < 0 || newQty > destination.MaxBookings)
        {
            throw new InvalidDestinationException("There are no available reservations for this trip, or you haven't booked anything yet. Please, try again later.");
        }

        bookings[destination.Name] = newQty;
    }
    
    private Destination? FindDestination(string destinationName)
    {
        return _destinations.FirstOrDefault(d => d != null && d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }
}