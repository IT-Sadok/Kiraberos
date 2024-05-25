namespace TravelAgencyService;

public class ActionSelectorService
{
    
    public void DisplayMenu()
    {
        Console.WriteLine("1. Book a ticket");
        Console.WriteLine("2. Cancel your reservation");
        Console.WriteLine("3. Display all current bookings");
        Console.WriteLine("4. Exit");
        Console.Write("Choose your action: ");
    }
}