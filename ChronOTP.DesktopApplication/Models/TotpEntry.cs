using ReactiveUI;

namespace ChronOTP.DesktopApplication.Models;

public class TotpEntry : ReactiveObject
{
    private int _remainingSeconds;
    
    public required string Name { get; set; }
    public required string Code { get; set; }

    public required int RemainingSeconds
    {
        get => _remainingSeconds; 
        set => this.RaiseAndSetIfChanged(ref _remainingSeconds, value);
    }
}