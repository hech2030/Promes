using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTeam.Common.Models;
using MyTeam.Common.Requests.Auth;
using MyTeam.Common.Requests.bo.Users;
using System;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> PostApplicationUser(RegisterRequest request)
        {
            User user;
            if (request.id != null)
            {
                user = await _userManager.FindByIdAsync(request.id);
                if (request.newPassword != null)
                {
                    var identityResult = await _userManager.ChangePasswordAsync(user, request.currentPassword, request.newPassword);
                    if (!identityResult.Succeeded)
                    {
                        return BadRequest(new { Message = identityResult.Errors.FirstOrDefault()?.Description });
                    }
                }
                user.UserName = request.username;
                user.Email = request.email;
                user.role = request.role;
                user.roleLabel = request.roleLabel;
                user.FullName = request.fullName;
                user.PhoneNumber = request.phoneNumber;
                if (request.image != null)
                {
                    user.Image = request.image;
                }
            }
            else
            {
                user = new User
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
                    var identityResult = await _userManager.CreateAsync(user, request.password);
                    if (identityResult.Succeeded)
                    {
                        return Ok(identityResult);
                    }
                    return BadRequest(new { Message = identityResult.Errors.FirstOrDefault()?.Description });
                }

                var ExistingUser = await _userManager.FindByIdAsync(user.Id);
                user.PasswordHash = ExistingUser.PasswordHash;
                var result = await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
                throw;
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
                        new Claim("UserId",user.Id.ToString())}),
                        Expires = DateTime.UtcNow.AddMinutes(50),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456789")), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token, user.UserName, user.roleLabel, user.role, user.Image });
                }
                return BadRequest(new { Message = "UserName or password is incorrect." });
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
        public async Task<ActionResult> FindUser(UserGetRequest Request)
        {
            try
            {
                var user = _userManager.Users;
                if (Request.FullUserName != null)
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
                var result = await user.ToListAsync();
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
            try
            {
                var user = await _userManager.FindByIdAsync(request.id);
                _ = await _userManager.DeleteAsync(user);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                bool success = false;
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { success });
            }
        }
    }
}