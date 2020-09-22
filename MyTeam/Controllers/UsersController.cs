using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTeam.Common.Models;
using MyTeam.Common.Requests.Auth;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UsersController(SignInManager<User> SignInManager, UserManager<User> userManager)
        {
            _signInManager = SignInManager;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<object> PostApplicationUser(RegisterRequest request)
        {
            var User = new User()
            {
                UserName = request.userName,
                Email = request.Email,
                NormalizedUserName = request.fullName
            };
            try
            {
                var result = await _userManager.CreateAsync(User, request.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest Request)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(Request.userName);
                if (user != null && await _userManager.CheckPasswordAsync(user, Request.password))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                new Claim("UserId",user.Id.ToString())
                }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456789")), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { Message = "UserName or password is incorrect." });
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}
