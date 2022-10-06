using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int? RoleId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }

        public Role Role { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
