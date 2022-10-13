using AutoMapper;
using EyeTrainer.Api.Contracts.EyeTrainingPlan;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Bootstrap.Profiles
{
    public class EyeTrainingPlanProfile : Profile
    {
        public EyeTrainingPlanProfile()
        {
            CreateMap<EyeTrainingPlan, EyeTrainingPlanResponse>();
            CreateMap<EyeTrainingPlan, PostEyeTrainingPlanRequest>()
                .ReverseMap();
            CreateMap<EyeTrainingPlan, PatchEyeTrainingPlanRequest>()
                .ReverseMap();
        }
    }
}
