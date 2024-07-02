using TravelAgencyService.Models;
using TravelAgencyService.Services;

namespace TravelAgencyService.Interfaces;

public interface IAgency
{
    void Book(Destination? destination, int qty, bool isAdd, Dictionary<string, BookingInfo> bookings);
    void DisplayBookings();
    void DisplayBookingsByDate();
}