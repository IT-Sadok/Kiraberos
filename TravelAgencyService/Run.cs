using TravelAgencyService.Models;

namespace TravelAgencyService;

public class Run
{
    private static TravelAgency _programm;

    static void Main()
    {
        _programm = new TravelAgency();
        _programm.Run();
    }
}