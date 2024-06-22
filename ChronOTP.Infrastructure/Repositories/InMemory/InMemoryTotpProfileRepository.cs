using ChronOTP.Core.Abstractions.Repositories;
using ChronOTP.Core.Models;

namespace ChronOTP.Infrastructure.Repositories.InMemory;

public class InMemoryTotpProfileRepository : ITotpProfileRepository
{
    private readonly List<TotpProfile> _profilesStorage = [];

    public Task<ICollection<TotpProfile>> GetAllProfiles()
    {
        return Task.FromResult<ICollection<TotpProfile>>(_profilesStorage);
    }

    public Task<TotpProfile?> GetProfileByName(string name)
    {
        var profile = _profilesStorage.FirstOrDefault(totpProfile => totpProfile.Name == name);
        
        return Task.FromResult(profile);
    }

    public Task CreateProfile(TotpProfile profile)
    {
        _profilesStorage.Add(profile);
        return Task.CompletedTask;
    }
}