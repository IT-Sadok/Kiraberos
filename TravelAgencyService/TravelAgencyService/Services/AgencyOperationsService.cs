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
        var destination = _consoleService.GetDestination();
            try
            {
                if (destination != null) travelAgency.BookTicket(destination);
                _consoleService.ShowMessage("Booking is successful.");
            }
            catch (Exception ex)
            {
                _consoleService.ShowMessage(ex.Message);
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
            if (bookings.Count == 0)
            {
                Console.WriteLine("There are no current bookings");
                return;
            }

        Console.WriteLine("Current reservations:");
        foreach (var destination in bookings)
        {
            if (destination != null)
                Console.WriteLine($"{destination?.Name}: {destination.CurrentBookings} destination(s) booked.");
        }
    }
}