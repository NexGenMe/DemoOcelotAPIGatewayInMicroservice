using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        
        private static ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();

        [HttpPost("login/{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {
            await Task.Delay(1000); // Simulate some async work
            var getEmail = _users!.Keys.Where(x => x.Equals(email)).FirstOrDefault();
            if (!string.IsNullOrEmpty(getEmail))
            {
                _users.TryGetValue(getEmail, out string? storedPassword);
                if(!Equals(storedPassword, password))
                    return BadRequest("Invalid password");

                string jwtToken = GenerateJwtToken(email);
                return Ok(jwtToken);
            }
            return NotFound("User not found");
        }

        private string GenerateJwtToken(string email)
        {
            var key = Encoding.UTF8.GetBytes(config["Authentication:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email!)
            };
            var token = new JwtSecurityToken(
                issuer: config["Authentication:Issuer"],
                audience: config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register/{email}/{password}")]
        public async Task<IActionResult> Register(string email, string password)
        {
            await Task.Delay(1000); // Simulate some async work
            if (_users.TryAdd(email, password))
            {
                _users[email] = password;
                return Ok("User registered successfully");
            }
            return BadRequest("User already exists");
        }
    }
}
