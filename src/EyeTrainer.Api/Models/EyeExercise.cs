using System.ComponentModel.DataAnnotations.Schema;

namespace EyeTrainer.Api.Models
{
    public class EyeExercise
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimesPerSet { get; set; }
        public int SetCount { get; set; }
        public double RestTimeSeconds { get; set; }

        [NotMapped]
        public TimeSpan RestTime => TimeSpan.FromSeconds(RestTimeSeconds); 

        public ICollection<EyeTrainingPlan> EyeTrainingPlans { get; set; }
    }
}
