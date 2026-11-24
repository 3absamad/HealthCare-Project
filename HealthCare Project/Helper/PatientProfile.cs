using AutoMapper;
using HealthCare_Project.Dtos.Patient;
using HealthCare_Project.Entites;

namespace HealthCare_Project.Helper
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientReadDto>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))

                .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth));
        }
    }
}
