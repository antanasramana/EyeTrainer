namespace EyeTrainer.Api.Contracts.Appointment
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
