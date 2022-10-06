using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeTrainer.Api.Models
{
    public class EyeExercise
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public int TimesPerSet { get; set; }
        public int SetCount { get; set; }
        public double RestTimeSeconds { get; set; }

        [NotMapped]
        public TimeSpan RestTime => TimeSpan.FromSeconds(RestTimeSeconds); 

        public ICollection<EyeTrainingPlan> EyeTrainingPlans { get; set; }
    }
}
