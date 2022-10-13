namespace EyeTrainer.Api.Contracts.Appointment
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
