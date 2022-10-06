namespace EyeTrainer.Api.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Speciality { get; set; }

        public List<Patient> Patients { get; set; }
    }
}
