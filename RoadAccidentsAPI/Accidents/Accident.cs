public class Accident
{
    public int Id {get; set;}
    public DateTime IncidentTime { get; set; }
    public string? Location { get; set; }
    public string?  Description { get; set; }
    public int NumberOfVehiclesInvolved { get; set; }
    public AccidentSeverity Severity { get; set; }
    public LightConditions lightConditions { get; set; }
    public int? NumberOfCasualties { get; set; }

}

public enum AccidentSeverity
{
    Minor,
    Moderate,
    Serious,
    Fatal
}

public enum LightConditions
{
    Light,
    Dark
}