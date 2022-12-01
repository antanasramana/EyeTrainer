namespace EyeTrainer.Api.Contracts.Appointment
{
    public class PatchAppointmentRequest
    {
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
    }
}
