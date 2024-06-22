using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using ChronOTP.Core.Abstractions.Repositories;
using ChronOTP.Core.Abstractions.Utils;
using ChronOTP.DesktopApplication.Helpers;
using ChronOTP.DesktopApplication.Models;
using ChronOTP.Infrastructure.Repositories.InMemory;
using ChronOTP.Infrastructure.Utils;
using ReactiveUI;

namespace ChronOTP.DesktopApplication.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ITotpProfileRepository _repository;
    private readonly ITotpGenerator _totpGenerator;
    private readonly Timer _timer;

    private bool _isShowCodeCopied = false;

    public MainWindowViewModel()
    {
        _repository = new InMemoryTotpProfileRepository();
        _totpGenerator = new TotpGenerator();
        _timer = new Timer();
    }
    
    public MainWindowViewModel(ITotpProfileRepository repository, ITotpGenerator totpGenerator)
    {
        _repository = repository;
        _totpGenerator = totpGenerator;
        _timer = new Timer(1000);

        _timer.Elapsed += (sender, args) => TickCodeEntries();
        _timer.Start();
    }

    public bool IsShowCodeCopied
    {
        get => _isShowCodeCopied;
        set => this.RaiseAndSetIfChanged(ref _isShowCodeCopied, value);
    }
    
    public ObservableCollection<TotpEntry> Entries { get; set; } = [];

    public async Task Initialize()
    {
        var profiles = await _repository.GetAllProfiles();

        Entries.Clear();
        
        foreach (var profile in profiles)
        {
            var totp = await _totpGenerator.GenerateTotp(profile.SecretKey);
            
            var codeEntry = new TotpEntry
            {
                Name = profile.Name,
                Code = totp.Value,
                RemainingSeconds = totp.RemainingSeconds
            };
            
            Entries.Add(codeEntry);
        }
    }

    private async void TickCodeEntries()
    {
        foreach (var entry in Entries)
        {
            if (entry.RemainingSeconds <= 1)
            {
                await Initialize();
                return;
            }
            
            entry.RemainingSeconds--;
        }
    }
    
    public async void CopyCodeToClipboard(TotpEntry profile)
    {
        await ClipboardHelper
            .GetClipboard()
            .SetTextAsync(profile.Code);
        
        IsShowCodeCopied = true;
        await Task.Delay(2000);
        IsShowCodeCopied = false;
    }
}