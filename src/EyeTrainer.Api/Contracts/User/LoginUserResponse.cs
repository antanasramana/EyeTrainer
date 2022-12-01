namespace EyeTrainer.Api.Contracts.User
{
    public class LoginUserResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
