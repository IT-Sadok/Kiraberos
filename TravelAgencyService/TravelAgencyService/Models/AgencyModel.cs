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
            _jsonServiceManager.SaveBookingToJson(destination, qty, true); 
    }

    public bool CancelBooking(string destinationName, int qty)
    {
        var destination = FindDestination(destinationName);
        if (destination == null)
        {
            return false;
        }
        _jsonServiceManager.SaveBookingToJson(destination, qty, false);
        return true;
    }

    public Dictionary<string, int> GetAllBookings()
    {
        return _jsonServiceManager.LoadBookings();
    }

    private Destination? FindDestination(string destinationName)
    {
        return _destinations.FirstOrDefault(d => d != null && d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }
}