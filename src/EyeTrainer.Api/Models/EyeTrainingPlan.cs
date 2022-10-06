namespace EyeTrainer.Api.Models
{
    public class EyeTrainingPlan
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public ICollection<EyeExercise> EyeExercises { get; set; }
    }
}
