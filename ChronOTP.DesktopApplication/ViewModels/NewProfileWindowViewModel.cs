using System.Threading.Tasks;
using ChronOTP.Core.Abstractions.Repositories;
using ChronOTP.Core.Models;
using ChronOTP.Infrastructure.Repositories.InMemory;
using ReactiveUI;

namespace ChronOTP.DesktopApplication.ViewModels;

public class NewProfileWindowViewModel : ViewModelBase
{
    private readonly ITotpProfileRepository _repository;
    
    /// <summary>
    /// Used just for preview purposes
    /// </summary>
    public NewProfileWindowViewModel()
    {
        _repository = new InMemoryTotpProfileRepository();
    }
    
    public NewProfileWindowViewModel(ITotpProfileRepository repository)
    {
        _repository = repository;
    }
    
    private string _displayName = string.Empty;
    private string  _secretKey = string.Empty;

    public string DisplayName
    {
        get => _displayName;
        set => this.RaiseAndSetIfChanged(ref _displayName, value);
    }

    public string SecretKey
    {
        get => _secretKey; 
        set => this.RaiseAndSetIfChanged(ref _secretKey, value);
    }

    public async Task AddProfileAsync()
    {
        if (string.IsNullOrWhiteSpace(_displayName))
        {
            return;
        }
        
        if (string.IsNullOrWhiteSpace(_secretKey))
        {
            return;
        }

        var duplicateProfile = await _repository.GetProfileByName(_displayName);

        if (duplicateProfile is not null)
        {
            return;
        }

        if (!ValidateSecretKey(_secretKey))
        {
            return;
        }
        
        var profile = new TotpProfile
        {
            Name = _displayName,
            SecretKey = _secretKey
        };

        await _repository.CreateProfile(profile);
    }

    private bool ValidateSecretKey(string secretKey)
    {
        for (var i = 0; i < secretKey.Length; i++)
        {
            if (!ValidateSecretKeyChar(secretKey[i]))
            {
                return false;
            }
        }

        return true;
    }

    private bool ValidateSecretKeyChar(char c)
    {
        int value = c;
        
        if (value is < 91 and > 64)
        {
            return true;
        }
        
        if (value is < 56 and > 49)
        {
            return true;
        }
        
        if (value is < 123 and > 96)
        {
            return true;
        }

        return false;
    }
}