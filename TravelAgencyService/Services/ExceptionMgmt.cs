namespace TravelAgencyService.Services;

public abstract class ExceptionMgmt : Exception
{
    public class InvalidDestinationException : Exception
    {
        public InvalidDestinationException() { }

        public InvalidDestinationException(string message) : base(message) { }

        public InvalidDestinationException(string message, Exception innerException) : base(message, innerException) { }
    }
}