using E_CommerceProject.Business.UserInfo.Models;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserInfoController(UserManager<ApplicationUser> _usermanager){
            userManager = _usermanager;
          }

        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> changeemail(ChangeEmail changeemail)
        {
            var user =await userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound();
            }
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var aaa=await userManager.CheckPasswordAsync(user, changeemail.Password);
            if (aaa) {
                var email=await userManager.FindByEmailAsync(changeemail.Newemail);
                if (email == null) {
                    var result = await userManager.ChangeEmailAsync(user, changeemail.Newemail, token);
                    if (!result.Succeeded) { return BadRequest(); }
                    return Ok("your email changed successfully");
                }
                else { return BadRequest("This email already has account"); }
                

            }
            else { return BadRequest("your password is not correct"); }
            
        }
        [HttpPost("ChangePhoneNum")]
        public async Task<ActionResult> changephonenum(ChangePhoneNum changephonenum)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var result = await userManager.ChangePhoneNumberAsync(user, changephonenum.Newpass, token);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok("your phone number changed successfully");

        }

        [HttpPost("ChangePass")]
        public async Task<IActionResult> changepassword(ChangePass changePass)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var result = await userManager.ChangePasswordAsync(user, changePass.Currentpassword, changePass.Newpassword);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok("your password changed successfully");
        }
       
    }
}
