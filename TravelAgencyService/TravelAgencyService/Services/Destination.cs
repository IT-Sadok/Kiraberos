namespace TravelAgencyService.Services;

    public class Destination(string name, int maxBookings)
    {
        public string Name { get; } = name;
        private int MaxBookings { get; } = maxBookings;
        public int CurrentBookings { get; private set; } = 0;

        public void BookTicket()
        {
            if (CurrentBookings >= MaxBookings)
            {
                throw new InvalidDestinationException("There are no available reservations for this trip. Please, try again later.");
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