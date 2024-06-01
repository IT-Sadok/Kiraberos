namespace TravelAgencyService.Services;

    public class Destination
    {
        public string Name { get; }
        private readonly int _maxBookings;
        public int CurrentBookings { get; private set; } = 0;


        public Destination(string name, int maxBookings)
        {
            Name = name;
            _maxBookings = maxBookings;

        }

        public void BookTicket()
        {
            if (CurrentBookings >= _maxBookings)
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