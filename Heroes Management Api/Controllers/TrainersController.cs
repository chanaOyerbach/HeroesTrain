namespace Heroes_Management_Api.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Heroes_Management_Api.Models;
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    [ApiController]
    [Route("api/[controller]")]
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TrainersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var trainer = _context.Trainers
                    .Include(t => t.Heroes)
                    .FirstOrDefault(t => t.Email == request.Email && t.Password == request.Password);

                if (trainer == null)
                {
                    Log.Warning("Unauthorized login attempt for email: {Email}", request.Email);
                    return Unauthorized();
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345_superSecureKey"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "*",
                    audience: "*",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                Log.Information("Login successful for email: {Email}", request.Email);

                return Ok(new { Token = token, Trainer = trainer });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during login for email: {Email}", request.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("this_is_a_32_character_long_secret_key!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}