using Microsoft.EntityFrameworkCore;

namespace RoadAccidentsAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Accident> Accidents {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accident>().ToTable("Accidents");
    }

    
}
