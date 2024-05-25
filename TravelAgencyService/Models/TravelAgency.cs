using TravelAgencyService.AbstractClasses;
using TravelAgencyService.Services;

namespace TravelAgencyService.Models;

class TravelAgency: AbstractTravelAgency
{
    private static ActionSelectorService _displaySelectorService;
    private AgencyOperationsService _agencyOperation;

    public override void Run()
    {
        var travelAgency = new AgencyModel();
        var exit = false;

        while (!exit)
        {
            _displaySelectorService = new ActionSelectorService();
            _agencyOperation = new AgencyOperationsService();
            _displaySelectorService.DisplayMenu();
            string choice = Console.ReadLine();
            
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


