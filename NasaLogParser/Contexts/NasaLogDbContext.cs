using Microsoft.EntityFrameworkCore;
using NasaLogParser.Entities.Data;

namespace NasaLogParser.Contexts;

public class NasaLogDbContext : DbContext
{
    private const string DbSchema = "log";
    private const string DbMigrationsHistoryTable = "__LogDbMigrationsHistory";

    public DbSet<LogRecord> LogRecords { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=localhost;Port=5432;Database=nasa;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        DbMigrationsHistoryTable,
                        DbSchema);
                })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbSchema);
    }
}