namespace EyeTrainer.Api.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public User Doctor { get; set; }
        public User Patient { get; set; }

        public ICollection<EyeTrainingPlan> EyeTrainingPlans { get; set; }
    }
}
