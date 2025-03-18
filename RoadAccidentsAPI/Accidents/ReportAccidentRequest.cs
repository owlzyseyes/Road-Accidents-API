using System;
using FluentValidation;

namespace RoadAccidentsAPI.Accidents;

public class ReportAccidentRequest
{ 
    public DateTime? IncidentTime { get; set; }
    public string? Location { get; set; }
    public string?  Description { get; set; }
    public int? NumberOfVehiclesInvolved { get; set; }
    public AccidentSeverity Severity { get; set; }
    public LightCondtions WeatherCondition { get; set; }
    public bool? IsFatalAccident { get; set; }
    public int? NumberOfCasualties { get; set; }
}

public class ReportAccidentRequestValidator : AbstractValidator<ReportAccidentRequest>
{
    public ReportAccidentRequestValidator()
    {
        RuleFor(x => x.IncidentTime)
            .NotNull()
            .Must(x => x <= DateTime.UtcNow)
            .WithMessage("Incident time cannot be in the future");

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.NumberOfVehiclesInvolved)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.NumberOfCasualties)
            .GreaterThanOrEqualTo(0)
            .When(x => x.NumberOfCasualties.HasValue);

        RuleFor(x => x.Severity)
            .IsInEnum();
    }
}

