using TravelAgencyService.Models;

namespace TravelAgencyService.Services;

public class AgencyOperationsService
{
    private readonly ConsoleService _consoleService;

    public AgencyOperationsService(ConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
    public void BookTicket(AgencyModel travelAgency)
    {
        const int maxRetryAttempts = 3;
        const int delayMilliseconds = 1000;
        var retryAttempts = 0;

        while (retryAttempts < maxRetryAttempts)
        {
            var destination = _consoleService.GetDestination();
            var qty = _consoleService.GetQty();
            try
            {
                if (destination != null) travelAgency.BookTicket(destination, qty);
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

    public void CancelBooking(AgencyModel travelAgency)
    {
        var destination = _consoleService.GetDestination();
        var qty = _consoleService.GetQty();
        try
        {
            _consoleService.ShowMessage(destination != null && travelAgency.CancelBooking(destination, qty)
                ? "Reservation canceled."
                : "Reservation(s) not found.");
        }
        catch (Exception exception)
        {
            _consoleService.ShowMessage(exception.Message);
        }
     
    }

    public void DisplayBookings(AgencyModel travelAgency)
    {
        var bookings = travelAgency.GetAllBookings();
        _consoleService.ShowBookings(bookings);
    }
}