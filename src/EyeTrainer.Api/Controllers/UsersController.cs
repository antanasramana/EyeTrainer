using AutoMapper;
using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Handlers;
using EyeTrainer.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace EyeTrainer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EyeTrainerApiContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthenticationHandler _authenticationHandler;

        public UsersController(EyeTrainerApiContext context, 
            IMapper mapper, 
            IAuthenticationHandler authenticationHandler)
        {
            _context = context;
            _mapper = mapper;
            _authenticationHandler = authenticationHandler;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var userToRegister = _mapper.Map<User>(registerUserRequest);
            var password = registerUserRequest.Password;

            var createdUser = await _authenticationHandler.TryRegisterUser(userToRegister, password);

            if (createdUser is null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            var registerUserResponse = _mapper.Map<RegisterUserResponse>(createdUser);

            return Ok(registerUserResponse);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest loginUserRequest)
        {
            var token = await _authenticationHandler.LoginUser(loginUserRequest.Email, loginUserRequest.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            var loginUserResponse = new LoginUserResponse { Token = token };

            return Ok(loginUserResponse);
        }

        [HttpGet]
        [Authorize(Policy = Roles.Administrator)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
