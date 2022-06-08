using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chat.DTOs;
using Chat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IdentityResult> Register(RegistrationDto model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.UserName, 
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        [HttpPost]
        [Route("/token")]
        public IActionResult Token(LoginDto loginDto)
        {
            var identity = GetIdentity(loginDto);

            if (identity.Result == null)
                return BadRequest( new { errorText = "Invalid password or username"});
            
            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Result.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Result.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(el =>
                el.UserName == loginDto.UserName);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!result)
                    return null;
                var claims = new List<Claim>
                {
                    new (ClaimsIdentity.DefaultNameClaimType, user.UserName)
                };
                
                ClaimsIdentity claimsIdentity = 
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
        
        [HttpGet]
        [Route("GetUserClaims")]
        public RegistrationDto GetUserClaims(string userName)
        {
            var user = _context.Users.FirstOrDefault(el => el.UserName == userName);
            RegistrationDto model = new RegistrationDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            
            return model;
        }
    }
}