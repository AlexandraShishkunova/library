//using libraryWeb.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace libraryWeb.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IConfiguration _configuration;


//        public AuthController(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//[HttpPost("register")]
//public async Task<IActionResult> Register([FromBody] RegisterDTO model)
//{

//    if (string.IsNullOrWhiteSpace(model.username))
//    {
//        return BadRequest(new { code = "InvalidUserName", description = "Username cannot be empty." });
//    }

//    var user = new ApplicationUser { UserName = model.username, Email = model.Email };
//    var result = await _userManager.CreateAsync(user, model.Password);

//    if (!result.Succeeded)
//    {
//        return BadRequest(result.Errors);
//    }

//    return Ok(new
//    {
//        message = "Registration successful",
//        userId = user.Id
//    });
//}

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginDTO model)
//        {
//            var user = await _userManager.FindByNameAsync(model.Username);
//            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//            {
//                var token = GenerateJwtToken(user);
//                return Ok(new { token, 
//                userId=user.Id});
//            }

//            return Unauthorized();
//        }

//        private string GenerateJwtToken(ApplicationUser user)
//        {

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        [Authorize]
//        [HttpGet("secure-data")]
//        public IActionResult GetSecureData()
//        {
//            return Ok(new { message = "This is protected data" });
//        }


//    }


//}


using libraryWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace libraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryContext _context; // Используем ваш контекст
        private readonly IConfiguration _configuration;

        public AuthController(LibraryContext context, IConfiguration configuration)
        {
            _context = context; // Инициализация контекста
            _configuration = configuration; // Инициализация конфигурации
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            // Проверка на пустое имя пользователя
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest(new { code = "InvalidUserName", description = "Username cannot be empty." });
            }

            // Проверка на существующего пользователя
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return BadRequest(new { code = "UserExists", description = "Username already exists." });
            }

            // Хэшируем пароль перед сохранением
            

            // Создание нового пользователя
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password // Сохраняем хэшированный пароль
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful", userId = user.id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            // Находим пользователя по имени
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user != null &&  user.Password==model.Password)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token, userId = user.id });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok(new { message = "This is protected data" });
        }
    }
}
