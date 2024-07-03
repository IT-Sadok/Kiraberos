using TravelAgencyService.Interfaces;
using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Decorators;

public class AgencyDecorator: IAgency
{
    private readonly FileService _fileService = new();
    private readonly AgencyModel _agencyModel;

    public AgencyDecorator(AgencyModel agencyModel)
    {
        _agencyModel = agencyModel;
    }
    
    public void Book(Destination? destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        
        if (isAdd)
        {
            _agencyModel.BookTicket(destination, qty);
        }
        else
        {
            _agencyModel.CancelBooking(destination, qty);
        }
        _agencyModel.Book(destination, qty, isAdd, bookings);
        _fileService.SaveBookingsToFile(bookings);
    }
    
    public void DisplayBookings()
    {
        _agencyModel.DisplayBookings();
    }

    public void DisplayBookingsByDate()
    {
        _agencyModel.DisplayBookingsByDate();
    }
}