using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        [MaxLength(50)]
        public string Speciality { get; set; }

        public User User { get; set; }
        public List<Patient> Patients { get; set; }
    }
}
