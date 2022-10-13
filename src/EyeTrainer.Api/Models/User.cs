using System.ComponentModel.DataAnnotations;
using EyeTrainer.Api.Constants;

namespace EyeTrainer.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string HashedPassword { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Role { get; set; } = Roles.Patient;
        public ICollection<Appointment> PatientVisits { get; set; }
        public ICollection<Appointment> DoctorVisits { get; set; }
    }
}
