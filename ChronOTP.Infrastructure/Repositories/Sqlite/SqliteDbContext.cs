using ChronOTP.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChronOTP.Infrastructure.Repositories.Sqlite;

public partial class SqliteDbContext : DbContext
{
    public DbSet<TotpProfile> Profiles { get; set; }

    public SqliteDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TotpProfile>(eb =>
            {
                eb.HasKey(p => p.Name);
                eb.Property(p => p.Name);
                eb.Property(p => p.SecretKey);
                eb.ToTable("Profiles");
            });
    }
}