using TravelAgencyService.Models;

namespace TravelAgencyService.Services;

public class AgencyOperationsService
{
       
    public void BookTicket(AgencyModel travelAgency)
    {
        Console.Write("Enter the name of the destination: ");
        var destination = Console.ReadLine();
            try
            {
                travelAgency.BookTicket(destination);
                Console.WriteLine("Booking is successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }

    public void CancelBooking(AgencyModel travelAgency)
    {
        Console.Write("Enter the name of the destination to cancel the reservation: ");
        var destination = Console.ReadLine();
        Console.WriteLine(destination != null && travelAgency.CancelBooking(destination)
            ? "Reservation canceled."
            : "Reservation(s) not found.");
    }

    public void DisplayBookings(AgencyModel travelAgency)
    {
        var bookings = travelAgency.GetAllBookings();
            if (bookings.Count == 0)
            {
                Console.WriteLine("There are no current bookings");
                return;
            }

        Console.WriteLine("Current reservations:");
        foreach (var destination in bookings)
        {
            if (destination != null)
                Console.WriteLine($"{destination?.Name}: {destination.CurrentBookings} destination(s) booked.");
        }
    }
}