using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Contracts.EyeTrainingPlan
{
    public class EyeTrainingPlanResponse
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesPerDay { get; set; }
        public string Description { get; set; }
    }
}
