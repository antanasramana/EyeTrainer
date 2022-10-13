using AutoMapper;
using EyeTrainer.Api.Contracts.User;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Bootstrap.Profiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, RegisterUserRequest>()
                .ReverseMap();
            CreateMap<User, RegisterUserResponse>();
        }
    }
}
