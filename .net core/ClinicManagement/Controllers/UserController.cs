using ClinicManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly ClinicManagementDBContext _context;

        public UserController(IConfiguration configuration, ClinicManagementDBContext con)
        {
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(configuration["Key"]!));
            _context = con;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserDto users)
        {
            string token = string.Empty;

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == users.UserName &&  u.Password == users.Password);
            if (user == null)
            {
                return "Invalid Username or Password!";
            }

            if(user.RoleID == 2)
            {
                var reqID = _context.Doctors.FirstOrDefault(a => a.UserID == user.UserID);

                var claims = new List<Claim>
                {
                    new Claim("Uname", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim("UserId",user.UserID.ToString()),
                    new Claim("User",reqID.DoctorID.ToString())
                };

                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials = cred
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(createToken);

                return Ok(new { token = token });
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("Uname", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim("UserId",user.UserID.ToString())
                };
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials = cred
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(createToken);

                return Ok(new { token = token });
            }

            return Ok();

        }
    }

    

    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
