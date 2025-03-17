using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace RoadAccidentsAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accident>().ToCollection("Accidents");
    }

    public DbSet<Accident> Accidents {get; set;}

    protected AppDbContext()
    {
    }
}
