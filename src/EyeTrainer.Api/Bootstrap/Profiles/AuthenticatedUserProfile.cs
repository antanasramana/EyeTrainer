using AutoMapper;
using EyeTrainer.Api.Contracts.User;
using EyeTrainer.Api.Handlers.Authentication;

namespace EyeTrainer.Api.Bootstrap.Profiles
{
    public class AuthenticatedUserProfile : Profile
    {
        public AuthenticatedUserProfile()
        {
            CreateMap<AuthenticatedUser, LoginUserResponse>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
