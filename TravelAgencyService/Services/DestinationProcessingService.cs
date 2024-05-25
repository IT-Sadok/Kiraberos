namespace TravelAgencyService;

public class DestinationProcessingService
{
    public class Destination
    {
        public string Name { get; }
        public int MaxBookings { get; }
        public int CurrentBookings { get; private set; }

        public Destination(string name, int maxBookings)
        {
            Name = name;
            MaxBookings = maxBookings;
            CurrentBookings = 0;
        }

        public void BookTicket()
        {
            if (CurrentBookings >= MaxBookings)
            {
                throw new Exception("There are no available reservations for this trip. Please, try again later.");
            }
            CurrentBookings++;
        }

        public bool CancelBooking()
        {
            if (CurrentBookings > 0)
            {
                CurrentBookings--;
                return true;
            }
            return false;
        }
    }
}