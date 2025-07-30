using System.Data;
using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using MedProSmile.Repository;
using MedProSmile.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IJwtTokenService _tokenService;
           
            private readonly IAuthService _authService;
        public AuthController(IJwtTokenService tokenService,IAuthService service)
            {
                _tokenService = tokenService;
                _authService = service;
            }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

        var user=_authService.GetUserDetails(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPut("forgotpassword")]
        public async Task<IActionResult> Update(string username,string newpassword)
        {
            if (username == null || newpassword==null || username=="" || newpassword=="") return BadRequest();
            await _authService.ForgotPassword(username,newpassword);
            return Ok("Succefully updated record !!");
        }


    }
    }

