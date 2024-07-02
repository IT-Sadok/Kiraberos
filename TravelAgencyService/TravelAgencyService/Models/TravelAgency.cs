using TravelAgencyService.AbstractClasses;
using TravelAgencyService.Decorators;
using TravelAgencyService.Interfaces;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

class TravelAgency: AbstractTravelAgency
{
    private ConsoleService _consoleService = new();
    private readonly FileService _fileService = new();
    private readonly Dictionary<string, BookingInfo> _allBookings;

    public TravelAgency()
    {
        _allBookings = _fileService.AllBookings;
    }

    public override void Run()
    {
        var travelAgency = new AgencyModel(_allBookings);
        IAgency decoratedAgency = new AgencyDecorator(travelAgency);
        var exit = false;
        _consoleService = new ConsoleService();

        while (!exit)
        {
            var choice = _consoleService.GetChoice();
            
            switch (choice)
            {
                case "1":
                    var bookingDestination = travelAgency.FindDestination(_consoleService.GetDestination());
                    var bookingQty = _consoleService.GetQty();
                    decoratedAgency.Book(bookingDestination, bookingQty, true, _allBookings);
                    break;
                case "2":
                    var cancelDestination = travelAgency.FindDestination(_consoleService.GetDestination());
                    var cancelQty = _consoleService.GetQty();
                    decoratedAgency.Book(cancelDestination, cancelQty, false, _allBookings);
                    break;
                case "3":
                    decoratedAgency.DisplayBookings();
                    break;
                case "4":
                    decoratedAgency.DisplayBookingsByDate();
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


