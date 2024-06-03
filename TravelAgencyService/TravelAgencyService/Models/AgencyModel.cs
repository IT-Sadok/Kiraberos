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
    public void BookTicket(string destinationName)
    {
        var destination = FindDestination(destinationName);

            if (destination == null)
            {
                throw new InvalidDestinationException("Wrong destination. Please, choose the right one.");
            }

        destination.BookTicket();
    }

    public bool CancelBooking(string destinationName)
    {
        var destination = FindDestination(destinationName);
        return destination != null && destination.CancelBooking();
    }

    public List<Destination?> GetAllBookings()
    {
        return _destinations.Where(d => d is { CurrentBookings: > 0 }).ToList();
    }

    private Destination? FindDestination(string destinationName)
    {
        return _destinations.FirstOrDefault(d => d != null && d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }
}