using AutoMapper;
using EyeTrainer.Api.Contracts.Appointment;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Bootstrap.Profiles
{
    public class AppointmentMapProfile : Profile
    {
        public AppointmentMapProfile()
        {
            CreateMap<Appointment, AppointmentResponse>();
            CreateMap<Appointment, PatchAppointmentRequest>()
                .ReverseMap();
            CreateMap<Appointment, PostAppointmentRequest>()
                .ReverseMap();
        }
    }
}
