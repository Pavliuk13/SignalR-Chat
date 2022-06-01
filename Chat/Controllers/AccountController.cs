using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("api/User/Register")]
        public async Task<IdentityResult> Register(AccountModel accountModel)
        {
            var user = new ApplicationUser()
            {
                UserName = accountModel.UserName,
                Email = accountModel.Email,
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName
            };

            var result = await _userManager.CreateAsync(user, accountModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return result;
            }
            
            return null;
        }
    }
}