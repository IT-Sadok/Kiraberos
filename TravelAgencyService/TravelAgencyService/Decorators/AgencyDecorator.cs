using TravelAgencyService.Interfaces;
using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Decorators;

public class AgencyDecorator: IAgency
{
    private readonly AgencyService _agencyService = new();
    
    public void Book(Destination destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        _agencyService.SaveBookingsToFile(bookings);
    }
}