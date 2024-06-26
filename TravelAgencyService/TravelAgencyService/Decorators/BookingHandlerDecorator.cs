using TravelAgencyService.Interfaces;
using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Decorators;

public class BookingHandlerDecorator: IBookingHandler
{
    private readonly AgencyService _agencyService = new();
    
    public void BeforeSaveBookingToJson(Destination destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings)
    {
        ValidateBookings(bookings, destination, qty, isAdd);
        _agencyService.SaveBookingsToFile(bookings);
    }

    private void ValidateBookings(Dictionary<string, BookingInfo> bookings, Destination destination, int qty, bool isAdd)
    {
        var currentQty = bookings.GetValueOrDefault(destination.Name, new BookingInfo());
        var date = DateTime.UtcNow;
        var newQty = isAdd ? currentQty.Quantity + qty : currentQty.Quantity - qty;
        
        if (newQty > destination.MaxBookings)
        {
            throw new InvalidDestinationException("There are no available reservations for this trip, or you haven't booked anything yet. Please, try again later.");
        }

        if (newQty <= 0)
        {
            bookings.Remove(destination.Name);
        }
        else
        {
            bookings[destination.Name] = new BookingInfo
            {
                Quantity = newQty,
                BookingDate = date
            };   
        }
    }
}