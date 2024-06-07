namespace TravelAgencyService.Services;

public class ConsoleService
{
    private InputValidationService _inputValidator;

    public string? GetChoice()
    {
        DisplayMenu();
        return Console.ReadLine();
    }

    public string? GetDestination()
    {
        Console.Write("Enter the name of the destination: ");
        return Console.ReadLine();
    }

    public int GetQty()
    {
        _inputValidator = new InputValidationService();
        return _inputValidator.GetValidatedIntegerInput("Enter the common qty: ");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowBookings(Dictionary<string, int> bookings)
    {
        if (bookings.Count == 0)
        {
            ShowMessage("There are no current bookings");
        }
        else
        {
            ShowMessage("Current reservations:");
            foreach (var booking in bookings)
            {
                ShowMessage($"Country: {booking.Key}, Bookings: {booking.Value} destination(s) booked.");
            }
        }
    }
    
    private void DisplayMenu()
    {
        Console.WriteLine("1. Book a ticket");
        Console.WriteLine("2. Cancel your reservation");
        Console.WriteLine("3. Display all current bookings");
        Console.WriteLine("4. Exit");
        Console.Write("Choose your action: ");
    }
}