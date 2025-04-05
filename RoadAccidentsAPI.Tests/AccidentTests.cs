using System.Net.Http.Json;
using System.Net;
using RoadAccidentsAPI.Accidents;


namespace RoadAccidentsAPI.Tests;

public class AccidentTests : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public AccidentTests(IntegrationTestWebAppFactory factory) : base(factory)
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
            IsFatal = true,
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
            IsFatal = false,
            NumberOfVehiclesInvolved = 0,
            lightConditions = LightConditions.Dark
        };

         // Act
        var response = await client.PostAsJsonAsync("/accidents", badaccidentRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

