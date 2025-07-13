using System.Threading.Tasks;
using JwtAuthDemo.DTOs;
using JwtAuthDemo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IJwt _jwt;

        public AuthController(IJwt jwt, IAuth auth)
        {
            _jwt = jwt;
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _auth.GetUserData(dto.Email, dto.Pass);

            if (result.Message == "Email and Password are required!")
                return BadRequest(new { message = result.Message });

            if (result.Message == "User not found!" || result.Message == "Invalid email or password!")
                return Unauthorized(new { message = "Invalid email or password!" });

            return Ok(new { message = "Login successful", result });
        }

        [HttpPost("jwt-login")]
        public async Task<IActionResult> JwtLogin([FromBody] LoginDTO dto)
        {
            var result = await _auth.Authentication(dto.Email, dto.Pass);

            if (result.Message == "Email and Password are required!")
                return BadRequest(new { message = result.Message });

            if (result.Message == "User not found!" || result.Message == "Invalid email or password!" || result.Data == null)
                return Unauthorized(new { message = "Invalid email or password!" });

            var userInfo = result.Data as AuthUserDTO;

            var token = _jwt.GenerateToken(userInfo.userId, userInfo.userName, userInfo.userEmail, userInfo.Pass, userInfo.userRole);

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                // Secure = true // Uncomment for production
            });

            return Ok(new { message = "Token generated", token });
        }

        [Authorize]
        [HttpGet("getUserData")]
        public async Task<IActionResult> GetUserData()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Authorization header is missing or invalid." });
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var result = await _jwt.ReadTokenData(token);
            return Ok(new { result });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("fetchAllUsers")]
        public IActionResult FetchAllUsers()
        {
            var users = _auth.GetUsers();
            return Ok(new { success = true, message = users });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok(new { success = true, message = "Logout successful!" });
        }
    }
}