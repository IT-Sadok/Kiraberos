using TravelAgencyService.AbstractClasses;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

class TravelAgency: AbstractTravelAgency
{
    private static ConsoleService? _consoleService;
    private AgencyOperationsService? _agencyOperation;

    public override void Run()
    {
        var travelAgency = new AgencyModel();
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
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Wrong choice. Please try again");
                    break;
            }
        }
    }
}


