namespace EyeTrainer.Api.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string ClinicalConditionDescription { get; set; }
        public int DoctorFk { get; set; }

        public Doctor Doctor { get; set; }
        public List<EyeTrainingPlan> EyeTrainingPlans { get; set; }
    }
}
