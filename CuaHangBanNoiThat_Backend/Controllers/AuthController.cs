using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CuaHangNoiThat_Backend.Controllers // Lưu ý: Đổi tên này nếu project của bạn có namespace khác
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Nạp cấu hình từ appsettings.json
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel request)
        {
            // TẠM THỜI TEST CỨNG TÀI KHOẢN (Bước tiếp theo chúng ta sẽ nối nó với Database thực tế)
            if (request.Username == "admin" && request.Password == "123")
            {
                // Nếu đúng tài khoản, tiến hành đúc Token với quyền Manager
                var token = GenerateJwtToken(request.Username, "Manager");
                return Ok(new { Message = "Đăng nhập thành công!", Token = token });
            }

            return Unauthorized("Sai tên đăng nhập hoặc mật khẩu!");
        }

        // Hàm xử lý thuật toán tạo ra chuỗi mã hóa JWT
        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            // Nhét thông tin người dùng và quyền hạn (Role) vào trong lõi của Token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // Token có hiệu lực trong 2 tiếng
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    // Class phụ trợ để hứng dữ liệu Username/Password từ giao diện Swagger
    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}