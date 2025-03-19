using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RoadAccidentsAPI;

public static class SeedData
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!await context.Accidents.AnyAsync())
        {
            await context.Accidents.AddRangeAsync(
                new Accident
                {
                    IncidentTime = DateTime.UtcNow.AddDays(-5),
                    Location = "Jua Kali",
                    Description = "Head-on collision",
                    NumberOfVehiclesInvolved = 2,
                    Severity = AccidentSeverity.Fatal,
                    lightConditions = LightConditions.Dark
                },
                new Accident
                {
                    IncidentTime = DateTime.UtcNow.AddDays(-7),
                    Location = "VOK",
                    Description = "Head-on collision",
                    NumberOfVehiclesInvolved = 2,
                    Severity = AccidentSeverity.Fatal,
                    lightConditions = LightConditions.Dark
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
