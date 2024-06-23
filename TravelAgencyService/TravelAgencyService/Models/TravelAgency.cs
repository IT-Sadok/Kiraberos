using TravelAgencyService.AbstractClasses;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

class TravelAgency: AbstractTravelAgency
{
    private static ConsoleService? _consoleService;
    private AgencyOperationsService? _agencyOperation;
    private readonly Dictionary<string, BookingInfo> _allBookings;
    private readonly JsonServiceManager _jsonServiceManager = new();

    public TravelAgency()
    {
        _allBookings = _jsonServiceManager.AllBookings;
    }

    public override void Run()
    {
        var travelAgency = new AgencyModel(_allBookings);
        var exit = false;
        _consoleService = new ConsoleService();
        _agencyOperation = new AgencyOperationsService(_consoleService);

        while (!exit)
        {
            var choice = _consoleService.GetChoice();
            switch (choice)
            {
                case "1":
                   _agencyOperation.BookTicket(travelAgency);
                    break;
                case "2":
                    _agencyOperation.CancelBooking(travelAgency);
                    break;
                case "3":
                    _agencyOperation.DisplayBookings(travelAgency);
                    break;
                case "4":
                    _agencyOperation.DisplayBookingsByDate(travelAgency);
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    _consoleService.ShowMessage("Wrong choice. Please try again");
                    break;
            }
        }
    }
}


