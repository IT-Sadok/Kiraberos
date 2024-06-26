using TravelAgencyService.AbstractClasses;
using TravelAgencyService.Decorators;
using TravelAgencyService.Interfaces;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

class TravelAgency: AbstractTravelAgency
{
    private ConsoleService _consoleService = new();
    private readonly AgencyService _agencyService = new();
    private readonly Dictionary<string, BookingInfo> _allBookings;

    public TravelAgency()
    {
        _allBookings = _agencyService.AllBookings;
    }

    public override void Run()
    {
        var travelAgency = new AgencyModel(_consoleService, _allBookings);
        var exit = false;
        _consoleService = new ConsoleService();

        while (!exit)
        {
            var choice = _consoleService.GetChoice();
            switch (choice)
            {
                case "1":
                   travelAgency.BookTicket();
                    break;
                case "2":
                    travelAgency.CancelBooking();
                    break;
                case "3":
                    travelAgency.DisplayBookings();
                    break;
                case "4":
                    travelAgency.DisplayBookingsByDate();
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


