using E_CommerceProject.Business.UserInfo.Models;
using E_CommerceProject.Infrastructure.Context;
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
        private readonly ECommerceContext _context;
        private readonly SignInManager<ApplicationUser> signInManager;


        public UserInfoController(UserManager<ApplicationUser> _usermanager, ECommerceContext context, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _usermanager;
            _context = context;
            signInManager = _signInManager;
        }


        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> changeemail(ChangeEmail changeemail)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            // edit user Email
            user.Email = changeemail.Newemail;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok(new { message = "your email changed successfully" });

        }
        [HttpPost("ChangePhoneNum")]
        public async Task<ActionResult> changephonenum(ChangePhoneNum changephonenum)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            // edit user PhoneNumber
            user.PhoneNumber = changephonenum.Newphonenum;
            var result = await userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            if (!result.Succeeded) { return BadRequest(); }
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var result = await userManager.ChangePhoneNumberAsync(user, changephonenum.Newphonenum,token);
            //if (!result.Succeeded) { return BadRequest(); }
            return Ok(new { message = "your phone number changed successfully" });
            //(new { message = "your email changed successfully" }

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
            return Ok(new { message = "You have been changed your password successfully" });
        }
        [HttpPost]
        [Route("signout")]
        //public async Task<IActionResult> SignOut()
        //{
        //    await signInManager.SignOutAsync();
        //    return Ok(new { message = "Signed out successfully" });
        //}
        //[HttpPost]
        //[Route("signout")]
        //public async Task<IActionResult> SignOut()
        //{

        //    await userManager.UpdateSecurityStampAsync(await userManager.GetUserAsync(User));
        //    return Ok(new { message = "You have been signed out successfully." });
        //}


        public async Task<IActionResult> signout()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.UpdateSecurityStampAsync(user);
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var result = await userManager.RemoveAuthenticationTokenAsync(user, "Bearer", token);
            Response.Headers.Remove("Authorization");
            if (!result.Succeeded) { return BadRequest(); }
            return Ok(new { message = "ok" });
        }
        
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            // Retrieve the current user
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Return user information
            var userInfo = new
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return Ok(userInfo);
        }
        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok(new { message = "your account deleted successfully" });
        }

    }
}
