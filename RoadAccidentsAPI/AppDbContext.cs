using Microsoft.EntityFrameworkCore;

namespace RoadAccidentsAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Accident> Accidents {get; set;}

    protected AppDbContext()
    {
    }
}
