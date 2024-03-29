using E_CommerceProject.Business.user.Dtos;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SignupController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }





        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegReq newuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser();
              user.Email = newuser.Email;
                   user.PhoneNumber = newuser.PhoneNumber;
                    user.UserName= newuser.UserName;
                    user.Country = newuser.country;

            var result = await _userManager.CreateAsync(user, newuser.Password);

            if (result.Succeeded)
            {
                

                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        


       



    }
}
