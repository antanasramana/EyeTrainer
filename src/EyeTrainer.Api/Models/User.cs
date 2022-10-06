namespace EyeTrainer.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int RoleFk { get; set; }
        public int? DoctorFk { get; set; }
        public int? PatientFk { get; set; }

        public Role Role { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
