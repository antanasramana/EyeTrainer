using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Models
{
    public class EyeTrainingPlan
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
        public ICollection<EyeExercise> EyeExercises { get; set; }
    }
}
