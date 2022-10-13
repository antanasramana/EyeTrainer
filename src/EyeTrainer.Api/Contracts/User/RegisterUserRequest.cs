using System.ComponentModel.DataAnnotations;

namespace EyeTrainer.Api.Contracts.User
{
    public class RegisterUserRequest
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
