using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Interfaces;

public interface IBookingHandler
{
    void BeforeSaveBookingToJson(Destination destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings);
}