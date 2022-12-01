using AutoMapper;
using EyeTrainer.Api.Bootstrap.Profiles;

namespace EyeTrainer.Api.Bootstrap
{
    public static class AutoMapperEngine
    {
        public static void SetupAutoMapper(this IServiceCollection serviceCollection)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapProfile());
                cfg.AddProfile(new AppointmentMapProfile());
                cfg.AddProfile(new EyeTrainingPlanProfile());
                cfg.AddProfile(new EyeExerciseProfile());
                cfg.AddProfile(new AuthenticatedUserProfile());
            });
            serviceCollection.AddSingleton(config.CreateMapper());
        }
    }
}
