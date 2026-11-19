using ConcertBooking.WebApp.Application.Contracts.Infrastructure.BarCode;
using QRCoder.Core.Generators;
using QRCoder.Core.Renderers;

namespace ConcertBooking.WebApp.Infrastructure.Barcode;

public class BarcodeService : IBarcodeService
{
    public Task<string> GenerateBarcodeAsync(string payload, CancellationToken cancellationToken)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        
        byte[] qrCodeBytes = qrCode.GetGraphic(20);
        
        return Task.FromResult<string>($"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}");
    }
}