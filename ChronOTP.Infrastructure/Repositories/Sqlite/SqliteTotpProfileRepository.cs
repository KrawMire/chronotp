using ChronOTP.Core.Abstractions.Repositories;
using ChronOTP.Core.Models;
using Microsoft.EntityFrameworkCore;
using OtpNet;

namespace ChronOTP.Infrastructure.Repositories.Sqlite;

public class SqliteTotpProfileRepository : ITotpProfileRepository
{
    private readonly SqliteDbContext _dbContext;
    
    public SqliteTotpProfileRepository(SqliteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<TotpProfile>> GetAllProfiles()
    {
        return await _dbContext.Profiles.ToListAsync();
    }

    public Task<TotpProfile?> GetProfileByName(string name)
    {
        return _dbContext.Profiles
            .Where(p => p.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task CreateProfile(TotpProfile profile)
    {
        _dbContext.Profiles.Add(profile);
        await _dbContext.SaveChangesAsync();
    }
}