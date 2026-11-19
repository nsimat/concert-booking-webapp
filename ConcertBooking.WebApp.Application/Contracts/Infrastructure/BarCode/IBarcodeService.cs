namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.BarCode;

public interface IBarcodeService
{
    Task<string> GenerateBarcodeAsync(string payload, CancellationToken cancellationToken);
}