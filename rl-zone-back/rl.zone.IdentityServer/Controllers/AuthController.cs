using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rl.zone.Dto.Auth;
using rl.zone.IdentityServer.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace rl.zone.IdentityServer.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(UserManager<AppUser> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm, FromBody] SignUpUserDto dto)
        {
            var user = new AppUser
            {
                FirstName = dto.FirstName,
                UserName = dto.Email,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var userCreateResult = await _userManager.CreateAsync(user, dto.Password);

            if (userCreateResult.Succeeded)
            {
                return new CreatedResult(string.Empty, string.Empty);
            }

            return new BadRequestResult();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm] SignInUserDto dto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == dto.Email);
            if (user is null)
            {
                return new NotFoundResult();
            }

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (result)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new JsonResult(GenerateJwt(user, roles));
            }

            return new BadRequestResult();
        }

        private string GenerateJwt(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpiresIn));
            
            var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Issuer,
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}