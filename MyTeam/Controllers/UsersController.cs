using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTeam.Common.Requests.Auth;
using MyTeam.Common.Requests.bo.Users;
using DataAcess.Models;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<user> _userManager;
        private SignInManager<user> _signInManager;

        public UsersController(SignInManager<user> SignInManager, UserManager<user> userManager)
        {
            _signInManager = SignInManager;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> PostApplicationUser(RegisterRequest request)
        {
            user User = null;
            if (request.id != null)
            {
                User = await _userManager.FindByIdAsync(request.id);
                if (request.newPassword != null)
                {
                    var test = await _userManager.ChangePasswordAsync(User, request.currentPassword, request.newPassword);
                    if(test.Succeeded== false)
                    {
                        return BadRequest(new { Message = test.Errors.FirstOrDefault().Description});
                    }
                }
                User.UserName = request.username;
                User.Email = request.email;
                User.role = request.role;
                User.roleLabel = request.roleLabel;
                User.FullName = request.fullName;
                User.PhoneNumber = request.phoneNumber;
                if (request.image!= null)
                {
                    User.Image = request.image;
                }
            }
            else
            {
                User = new user
                {
                    UserName = request.username,
                    Email = request.email,
                    FullName = request.fullName,
                    role = request.role,
                    roleLabel = request.roleLabel,
                };
            }
            try
            {
                if (request.password != null)
                {
                    var result = await _userManager.CreateAsync(User, request.password);
                    return Ok(result);
                }
                else
                {
                    var ExistingUser = await _userManager.FindByIdAsync(User.Id);
                    User.PasswordHash = ExistingUser.PasswordHash;
                    var result = await _userManager.UpdateAsync(User);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
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
                    return Ok(new { token, user.UserName, user.roleLabel, user.role, user.Image });
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

        [HttpPost]
        [Authorize]
        [Route("FindUser")]
        public ActionResult FindUser(UserGetRequest Request)
        {
            try
            {
                var user = _userManager.Users;
                if(Request.FullUserName!=null)
                {
                    user = user.Where(x => x.UserName == Request.FullUserName);
                }
                if (Request.id != null)
                {
                    user = user.Where(x => x.Id == Request.id);
                }
                if (Request.UserName != null)
                {
                    user = user.Where(x => x.FullName.Contains(Request.UserName));
                }
                if (Request.UserRole > 0)
                {
                    user = user.Where(x => x.role == Request.UserRole);
                }
                var result = user.ToList();
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUser(UserDeleteRequest request)
        {
            bool success = false;
            try
            {
                var user = await _userManager.FindByIdAsync(request.id);
                var result = await _userManager.DeleteAsync(user);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { success });
            }
        }
    }
}
