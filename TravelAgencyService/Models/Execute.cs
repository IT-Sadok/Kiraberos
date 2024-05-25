namespace TravelAgencyService;

class Execute: ITravelAgency
{
    private static ActionSelectorService _displaySelectorService;

    public override void Run()
    {
        AgencyModel travelAgency = new AgencyModel();
        bool exit = false;

        while (!exit)
        {
            _displaySelectorService = new ActionSelectorService();
            _displaySelectorService.DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new AgencyOperationsService().BookTicket(travelAgency);
                    break;
                case "2":
                    new AgencyOperationsService().CancelBooking(travelAgency);
                    break;
                case "3":
                    new AgencyOperationsService().DisplayBookings(travelAgency);
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


