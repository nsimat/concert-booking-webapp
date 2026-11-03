namespace ConcertBooking.WebApp.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"{name} with key ({key}) is not fouund!")
    {
        
    }
}