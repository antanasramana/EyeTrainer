using AutoMapper;
using EyeTrainer.Api.Contracts.EyeExercise;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Bootstrap.Profiles
{
    public class EyeExerciseProfile : Profile
    {
        public EyeExerciseProfile()
        {
            CreateMap<EyeExercise, EyeExerciseResponse>();
            CreateMap<EyeExercise, PostEyeExerciseRequest>()
                .ReverseMap();
            CreateMap<EyeExercise, PatchEyeExerciseRequest>()
                .ReverseMap();
        }
    }
}
