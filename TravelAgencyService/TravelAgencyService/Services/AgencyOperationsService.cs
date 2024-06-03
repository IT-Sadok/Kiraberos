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
            try
            {
                if (destination != null) travelAgency.BookTicket(destination);
                _consoleService.ShowMessage("Booking is successful.");
                break;
            }
            catch (Exception ex)
            {
                _consoleService.ShowMessage($"Attempt {retryAttempts + 1} failed: {ex.Message}");
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
        _consoleService.ShowMessage(destination != null && travelAgency.CancelBooking(destination)
            ? "Reservation canceled."
            : "Reservation(s) not found.");
    }

    public void DisplayBookings(AgencyModel travelAgency)
    {
        var bookings = travelAgency.GetAllBookings();
        _consoleService.ShowBookings(bookings);
    }
}