namespace EyeTrainer.Api.Contracts.EyeTrainingPlan
{
    public class PatchEyeTrainingPlanRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesPerDay { get; set; }
        public string Description { get; set; }
        public int? AppointmentId { get; set; }
    }
}
