using AutoMapper;
using RoadAccidentsAPI.Accidents;

public class AccidentsMappingProfile : Profile
{
    public AccidentsMappingProfile()
    {
        CreateMap<ReportAccidentRequest, Accident>();
        CreateMap<Accident, GetAccidentReport>();
    }
}