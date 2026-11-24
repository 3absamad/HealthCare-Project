using AutoMapper;
using HealthCare_Project.Dtos.Doctor;
using HealthCare_Project.Entites;

namespace HealthCare_Project.Helper
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorReadDto>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))

                .ForMember(dest => dest.Specialization,
                opt => opt.MapFrom(src => src.Specialization));   

        }
    }
}
