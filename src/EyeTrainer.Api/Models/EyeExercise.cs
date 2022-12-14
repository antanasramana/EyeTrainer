using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Models
{
    public class EyeExercise
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimesPerSet { get; set; }
        public int SetCount { get; set; }
        public double RestTimeSeconds { get; set; }

        public int? EyeTrainingPlanId { get; set; }
        public EyeTrainingPlan EyeTrainingPlan { get; set; }
    }
}
