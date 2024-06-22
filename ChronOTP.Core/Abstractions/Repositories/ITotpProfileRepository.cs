using ChronOTP.Core.Models;

namespace ChronOTP.Core.Abstractions.Repositories;

public interface ITotpProfileRepository
{
    Task<ICollection<TotpProfile>> GetAllProfiles();
    Task<TotpProfile?> GetProfileByName(string name);
    Task CreateProfile(TotpProfile profile);
}