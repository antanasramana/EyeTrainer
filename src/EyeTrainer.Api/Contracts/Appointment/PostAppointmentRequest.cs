namespace EyeTrainer.Api.Contracts.Appointment
{
    public class PostAppointmentRequest
    {
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
    }
}
