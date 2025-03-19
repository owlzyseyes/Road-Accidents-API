using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using RoadAccidentsAPI.Accidents;
using Humanizer;

namespace RoadAccidentsAPI.Tests;

public class BasicTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public BasicTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllAccidents_ReturnsOkResult()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/accidents");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task ReportAccident_ReturnsCreatedResult()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        var accidentRequest = new ReportAccidentRequest
        {
            IncidentTime = DateTime.UtcNow,
            Location = "Test Location",
            Description = "Test accident for unit testing",
            Severity = AccidentSeverity.Minor,
            NumberOfVehiclesInvolved = 2,
            lightConditions = LightConditions.Dark
        };

        // Act
        var response = await client.PostAsJsonAsync("/accidents", accidentRequest);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}