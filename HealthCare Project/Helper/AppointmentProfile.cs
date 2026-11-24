using AutoMapper;
using HealthCare_Project.Dtos.Appointment;
using HealthCare_Project.Entites;

namespace HealthCare_Project.Helper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {

            CreateMap<Appointment, AppointmentReadDto>()
                .ForMember(dest => dest.PatientId,
                opt => opt.MapFrom(src => src.PatientId))

                .ForMember(dest => dest.PatientName,
                opt => opt.MapFrom(src => src.Patient.Name))

                .ForMember(dest => dest.DoctorId,
                opt => opt.MapFrom(src => src.DoctorId))

                .ForMember(dest => dest.DoctorName,
                opt => opt.MapFrom(src => src.Doctor.Name))

                .ForMember(dest => dest.AppointmentDate,
                opt => opt.MapFrom(src => src.AppointmentDate));
        }
    }
}
