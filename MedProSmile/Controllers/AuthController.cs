using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IJwtTokenService _tokenService;

            public AuthController(IJwtTokenService tokenService)
            {
                _tokenService = tokenService;
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginRequest request)
            {
                // For demo: Mock user (replace with DB check using Dapper)
                var user = ValidateUser(request.Username, request.Password);

                if (user == null)
                    return Unauthorized("Invalid credentials");

                var token = _tokenService.GenerateToken(user);
                return Ok(new { token });
            }

            private User ValidateUser(string username, string password)
            {
                // Replace with stored procedure + Dapper lookup
                if (username == "admin" && password == "123")
                    return new User { Username = "admin", Password = "123", Role = "Admin" };
                if (username == "user" && password == "123")
                    return new User { Username = "user", Password = "123", Role = "User" };

                return null;
            }
        }
    }

