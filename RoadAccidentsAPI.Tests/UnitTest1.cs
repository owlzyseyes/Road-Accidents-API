using System.Net.Http.Json;
using System.Net;
using RoadAccidentsAPI.Accidents;

namespace RoadAccidentsAPI.Tests;

public class BasicTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IntegrationTestWebAppFactory _factory;

    public BasicTests(IntegrationTestWebAppFactory factory)
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
    public async Task GetAccidentById()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/accidents/1");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Failed with status code: {response.StatusCode}. Error: {content}");
        }
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
            IsFatal = false,
            NumberOfVehiclesInvolved = 2,
            lightConditions = LightConditions.Dark
        };

        // Act
        var response = await client.PostAsJsonAsync("/accidents", accidentRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task ReportInvalidAccidentData_ReturnsBadRequest()
    {
        // Arrange
        HttpClient client =_factory.CreateClient();
        var badaccidentRequest = new ReportAccidentRequest
        {
            IncidentTime = DateTime.UtcNow.AddDays(5),
            Location = "",
            Description = "",
            IsFatal = true,
            NumberOfVehiclesInvolved = 0,
            lightConditions = LightConditions.Dark
        };

         // Act
        var response = await client.PostAsJsonAsync("/accidents", badaccidentRequest);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}
