using MultiApi.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace MultiApi.Database;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Party> Party { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}