using TravelAgencyService.Interfaces;
using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Decorators;

public class AgencyDecorator: IAgency
{
    private readonly FileService _fileService = new();
    private readonly AgencyModel _agencyModel;

    public AgencyDecorator()
    {
        _agencyModel = new AgencyModel(_fileService.AllBookings);
    }
    
    public void Book(Destination destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        _agencyModel.Book(destination, qty, isAdd, bookings);
        _fileService.SaveBookingsToFile(bookings);
    }
}