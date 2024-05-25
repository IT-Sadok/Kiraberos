namespace TravelAgencyService;

public class AgencyModel
{
    private readonly List<DestinationProcessingService.Destination> _destinations;

    public AgencyModel()
    {
        _destinations = new List<DestinationProcessingService.Destination>
        {
            new DestinationProcessingService.Destination("Spain", 5),
            new DestinationProcessingService.Destination("Ukraine", 3),
            new DestinationProcessingService.Destination("Japan", 4),
            new DestinationProcessingService.Destination("Norway", 2),
            new DestinationProcessingService.Destination("New Zealand", 3)
        };
    }

    public void BookTicket(string destinationName)
    {
        var destination = FindDestination(destinationName);

        if (destination == null)
        {
            throw new Exception("Wrong destination. Please, choose the right one.");
        }

        destination.BookTicket();
    }

    public bool CancelBooking(string destinationName)
    {
        var destination = FindDestination(destinationName);

        if (destination != null)
        {
            return destination.CancelBooking();
        }
        return false;
    }

    public void DisplayBookings()
    {
        var bookings = _destinations.Where(d => d.CurrentBookings > 0).ToList();

        if (!bookings.Any())
        {
            Console.WriteLine("There are no current bookings");
            return;
        }

        Console.WriteLine("Current reservations:");
        foreach (var destination in bookings)
        {
            Console.WriteLine($"{destination.Name}: {destination.CurrentBookings} destination(s) booked.");
        }
    }

    private DestinationProcessingService.Destination FindDestination(string destinationName)
    {
        return _destinations.FirstOrDefault(d => d.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));
    }
}