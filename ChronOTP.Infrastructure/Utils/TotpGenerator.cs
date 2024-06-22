using ChronOTP.Core.Abstractions.Utils;
using ChronOTP.Core.Models;
using OtpNet;

namespace ChronOTP.Infrastructure.Utils;

public class TotpGenerator : ITotpGenerator
{
    public Task<TotpCode> GenerateTotp(string secretKey)
    {
        var secretKeyBytes = Base32Encoding.ToBytes(secretKey);
        var totp = new Totp(
            secretKeyBytes, 
            step: 30, 
            mode: OtpHashMode.Sha1,
            totpSize: 6);

        var code = new TotpCode(
            totp.ComputeTotp(DateTime.UtcNow),
            totp.RemainingSeconds());
        
        return Task.FromResult(code);
    }
}