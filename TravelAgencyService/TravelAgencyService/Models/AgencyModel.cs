using TravelAgencyService.Decorators;
using TravelAgencyService.Interfaces;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

public class AgencyModel(ConsoleService consoleService, Dictionary<string, BookingInfo> allBookings) : IBookingHandler
{
    private readonly List<Destination?> _destinations =
    [
        new Destination("Spain", 5),
        new Destination("Ukraine", 3),
        new Destination("Japan", 4),
        new Destination("Norway", 2),
        new Destination("New Zealand", 3)
    ];

    public void BookTicket()
    {
        const int maxRetryAttempts = 3;
        const int delayMilliseconds = 1000;
        var retryAttempts = 0;

        while (retryAttempts < maxRetryAttempts)
        {
            var destination = consoleService.GetDestination();
            var qty = consoleService.GetQty();
            try
            {
                if (destination != null) HandleBooking(destination, qty);
                consoleService.ShowMessage("Booking is successful.");
                break;
            }
            catch (Exception exception)
            {
                consoleService.ShowMessage($"Attempt {retryAttempts + 1} failed: {exception.Message}");
                retryAttempts++;
                if (retryAttempts < maxRetryAttempts)
                {
                    Thread.Sleep(delayMilliseconds);
                }
                else
                {
                    consoleService.ShowMessage("All attempts failed. Booking was not successful.");
                }
            }   
        }
    }

    private void HandleBooking(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);

            if (destination == null)
            {
                throw new InvalidDestinationException("Wrong destination. Please, choose the right one.");
            }
            BeforeSaveBookingToJson(destination, qty, true, allBookings); 
    }
    
    public void CancelBooking()
    {
        var destination = consoleService.GetDestination();
        var qty = consoleService.GetQty();
        try
        {
            consoleService.ShowMessage(destination != null && HandleCanceling(destination, qty)
                ? "Reservation canceled."
                : "Reservation(s) not found.");
        }
        catch (Exception exception)
        {
            consoleService.ShowMessage(exception.Message);
        }
     
    }

    private bool HandleCanceling(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);
        if (destination == null)
        {
            return false;
        }
        BeforeSaveBookingToJson(destination, qty, false, allBookings); 
        return true;
    }
    
    public void DisplayBookings()
    {
        var bookings = GetAllBookings();
        consoleService.ShowBookings(bookings);
    }
    
    public void DisplayBookingsByDate()
    {
        var date = consoleService.GetDate();
        var bookings = GetAllBookingsByDate(date);
        consoleService.ShowBookings(bookings);
    }
    
    private Destination? FindDestination(string destinationName)
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

    public void BeforeSaveBookingToJson(Destination destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        var agencyService = new BookingHandlerDecorator();
        agencyService.BeforeSaveBookingToJson(destination, qty, isAdd, bookings);
    }
}