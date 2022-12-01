using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Handlers.Authentication
{
    public class AuthenticatedUser
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
