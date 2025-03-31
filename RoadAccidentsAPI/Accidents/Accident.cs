using System.ComponentModel.DataAnnotations.Schema;

public class Accident
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public DateTime IncidentTime { get; set; }
    public string? Location { get; set; }
    public string?  Description { get; set; }
    public int NumberOfVehiclesInvolved { get; set; }
    public bool IsFatal { get; set; }
    public LightConditions lightConditions { get; set; }
    public int? NumberOfCasualties { get; set; }

}

public enum LightConditions
{
    Light,
    Dark
}