namespace TravelAgencyService.Services;

public class ConsoleService
{
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

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowBookings(List<Destination> bookings)
    {
        
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