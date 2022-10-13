namespace EyeTrainer.Api.Contracts.EyeExercise
{
    public class EyeExerciseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimesPerSet { get; set; }
        public int SetCount { get; set; }
        public double RestTimeSeconds { get; set; }
    }
}
