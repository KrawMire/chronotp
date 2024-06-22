using ChronOTP.Core.Models;

namespace ChronOTP.Core.Abstractions.Utils;

public interface ITotpGenerator
{
    Task<TotpCode> GenerateTotp(string secretKey);
}