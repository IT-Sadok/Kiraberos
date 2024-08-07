using TravelAgencyService.Interfaces;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

public class AgencyModel(Dictionary<string, BookingInfo> allBookings) : IAgency
{
    private readonly ConsoleService _consoleService = new();

    private readonly List<Destination?> _destinations =
    [
        new Destination("Spain", 5),
        new Destination("Ukraine", 3),
        new Destination("Japan", 4),
        new Destination("Norway", 2),
        new Destination("New Zealand", 3)
    ];
    
    public void BookTicket(Destination? destination, int qty)
    {
        const int maxRetryAttempts = 3;
        const int delayMilliseconds = 1000;
        var retryAttempts = 0;

        while (retryAttempts < maxRetryAttempts)
        {
            try
            {
                HandleBooking(destination, qty);
                _consoleService.ShowMessage("Booking is successful.");
                break;
            }
            catch (Exception exception)
            {
                _consoleService.ShowMessage($"Attempt {retryAttempts + 1} failed: {exception.Message}");
                retryAttempts++;
                if (retryAttempts < maxRetryAttempts)
                {
                    Thread.Sleep(delayMilliseconds);
                }
                else
                {
                    _consoleService.ShowMessage("All attempts failed. Booking was not successful.");
                }
            }   
        }
    }
    
    public void CancelBooking(Destination? destination, int qty)
    {
        try
        {
            _consoleService.ShowMessage(destination != null && HandleCanceling(destination, qty)
                ? "Reservation canceled."
                : "Reservation(s) not found.");
        }
        catch (Exception exception)
        {
            _consoleService.ShowMessage(exception.Message);
        }
    }
    
    public void Book(Destination? destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        if (destination != null)
            try
            {
                ValidateBookings(bookings, destination, qty, isAdd);
            }
            catch (Exception exception)
            {
                _consoleService.ShowMessage(exception.Message);
            }
    }
    
    private void ValidateBookings(Dictionary<string, BookingInfo> bookings, Destination destination, int qty, bool isAdd)
    {
        var currentQty = bookings.GetValueOrDefault(destination.Name, new BookingInfo());
        var date = DateTime.UtcNow;
        var newQty = isAdd ? currentQty.Quantity + qty : currentQty.Quantity - qty;
        
        if (newQty > destination.MaxBookings)
        {
            throw new InvalidDestinationException("There are no available reservations for this trip, or you haven't booked anything yet. Please, try again later.");
        }

        if (newQty <= 0)
        {
            bookings.Remove(destination.Name);
        }
        else
        {
            bookings[destination.Name] = new BookingInfo
            {
                Quantity = newQty,
                BookingDate = date
            };   
        }
    }
    
    public void DisplayBookings()
    {
        var bookings = GetAllBookings();
        _consoleService.ShowBookings(bookings);
    }
    
    public void DisplayBookingsByDate()
    {
        var date = _consoleService.GetDate();
        var bookings = GetAllBookingsByDate(date);
        _consoleService.ShowBookings(bookings);
    }

    private void HandleBooking(Destination? destination, int qty)
    {

        if (destination == null)
        {
            throw new InvalidDestinationException("Wrong destination. Please, choose the right one.");
        }
    }
    
    private bool HandleCanceling(Destination? destination, int qty)
    {
        if (destination == null)
        {
            return false;
        }
        return true;
    }
    
    public Destination? FindDestination(string? destinationName)
    {
        return _destinations.FirstOrDefault(d => d != null && d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }

    private Dictionary<string, BookingInfo> GetAllBookings()
    {
        return allBookings;
    }

    private Dictionary<string, BookingInfo> GetAllBookingsByDate(DateTime date)
    {
        return GetAllBookings()
            .Where(booking => booking.Value.BookingDate.Date == date.Date)
            .ToDictionary(booking => booking.Key, booking => booking.Value);
    }
}