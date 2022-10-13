using EyeTrainer.Api.Data;
using EyeTrainer.Api.Models;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EyeTrainer.Api.Handlers
{
    public interface IAuthenticationHandler
    {
        Task<User> TryRegisterUser(User user, string password);
        Task<string> LoginUser(string email, string password);
    }

    public class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly EyeTrainerApiContext _context;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthenticationHandler(EyeTrainerApiContext context,
            ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _context = context;
        }
        public async Task<User> TryRegisterUser(User user, string password)
        {
            user.DateOfRegistration = DateTime.Now;
            user.HashedPassword = BCryptNet.HashPassword(password);

            var currentUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (currentUser is not null) return null;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginUser (string email, string password)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser is null) return null;

            var isValidPassword = BCryptNet.Verify(password, existingUser.HashedPassword);
            if (!isValidPassword) return null;

            var token = _tokenGenerator.GenerateToken(existingUser.Id, existingUser.Role);

            return token;
        }
    }
}
