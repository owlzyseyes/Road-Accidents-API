public class Accident
{
    public int Id {get; set;}
    public DateTime IncidentDateTime { get; set; }
    public DateTime ReportedAt { get; set; }
    public string? Location { get; set; }
    public string?  Description { get; set; }
    public int NumberOfVehiclesInvolved { get; set; }
    public AccidentSeverity Severity { get; set; }
    public LightCondtions WeatherCondition { get; set; }
    public bool? IsFatalAccident { get; set; }
    public int? NumberOfCasualties { get; set; }

}

public enum AccidentSeverity
{
    Minor,
    Moderate,
    Serious,
    Fatal
}

public enum RoadType
{
    Highway,
    Urban,
    Country
}

public enum LightCondtions
{
    Light,
    Dark
}