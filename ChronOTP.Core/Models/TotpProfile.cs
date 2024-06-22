namespace ChronOTP.Core.Models;

public class TotpProfile
{
    public required string Name { get; set; }
    public required string SecretKey { get; set; }
}