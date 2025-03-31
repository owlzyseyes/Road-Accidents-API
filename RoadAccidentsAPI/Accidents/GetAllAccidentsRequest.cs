using System;

namespace RoadAccidentsAPI.Accidents;

public class GetAllAccidentsRequest
{
    public int Id {get; set;}
    public DateTime IncidentTime { get; set; }
    public string? Location { get; set; }
    public string?  Description { get; set; }
    public int NumberOfVehiclesInvolved { get; set; }
    public bool IsFatal { get; set; }
    public LightConditions lightConditions { get; set; }
    public int? NumberOfCasualties { get; set; }
    
}
