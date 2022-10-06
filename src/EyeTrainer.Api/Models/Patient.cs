namespace EyeTrainer.Api.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string ClinicalConditionDescription { get; set; }
        public int DoctorId { get; set; }

        public User User { get; set; }
        public Doctor Doctor { get; set; }
        public List<EyeTrainingPlan> EyeTrainingPlans { get; set; }
    }
}
