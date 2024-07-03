namespace TravelAgencyService.Models;

    public class Destination
    {
        public string Name { get; }
        public readonly int MaxBookings;

        public Destination(string name, int maxBookings)
        {
            Name = name;
            MaxBookings = maxBookings;
        }
    }