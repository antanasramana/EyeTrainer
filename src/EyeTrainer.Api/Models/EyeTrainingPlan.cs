namespace EyeTrainer.Api.Models
{
    public class EyeTrainingPlan
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesPerDay { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public ICollection<EyeExercise> EyeExercises { get; set; }
    }
}
