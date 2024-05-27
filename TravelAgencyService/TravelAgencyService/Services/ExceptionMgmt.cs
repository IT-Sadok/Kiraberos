namespace TravelAgencyService.Services;

public class InvalidDestinationException : Exception
{
    public InvalidDestinationException() { }

    public InvalidDestinationException(string message) : base(message) { }

    public InvalidDestinationException(string message, Exception innerException) : base(message, innerException) { }
    }