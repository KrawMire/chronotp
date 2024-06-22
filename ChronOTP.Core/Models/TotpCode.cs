namespace ChronOTP.Core.Models;

public record TotpCode(string Value, int RemainingSeconds);