using E_CommerceProject.Business.Emails.Dtos;
using E_CommerceProject.Business.Emails.Interfcaes;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = E_CommerceProject.Business.Emails.Dtos.Response;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //Check if user exists in db
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            //Check if password is correct
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userrole = await _userManager.GetRolesAsync(user);

                //claimlist creation
                var authenticationClaims = new List<Claim>
         {
             
             new Claim("uid",user.Id.ToString()),
             new Claim(ClaimTypes.Email,user.Email),
             new Claim(ClaimTypes.NameIdentifier,user.Id),
             new Claim(ClaimTypes.Role, "customer"),
             new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),

                //global user id unique
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
         };
                var userRoles = await _userManager.GetRolesAsync(user);
                //Add role to claims
                foreach (var role in userRoles)
                {
                    authenticationClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //Generate the token with claims 
                var jwtToken = GetToken(authenticationClaims);

                //return the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authenticationClaims)
        {
            var authenticationSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your_secret_key_herebsdgghsghbqgugs"));

            var Token = new JwtSecurityToken(
                   
                    expires: DateTime.Now.AddDays(2).ToLocalTime(),
                    claims: authenticationClaims,
                    signingCredentials: new SigningCredentials(authenticationSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return Token;
        }

        //[HttpPost("ForgetPassword")]
        //public async Task<IActionResult> ForgotPassword([Required] string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //        var forgotpasswordlink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
        //        var message = new Message(new string[] { user.Email! }, "Confirmation email link", forgotpasswordlink!);
        //        _emailService.SendEmail(message);

        //        return StatusCode(StatusCodes.Status200OK,

        //                new Response { Status = "success", Message = $"Link to reset password sent successfully to email {user.Email} ",tokenn=$"token={token}"});
        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest);
        //}

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var forgotpasswordlink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", forgotpasswordlink!);
                _emailService.SendEmail(message);

                // Include token in the response object
                var response = new Response
                {
                    Status = "success",
                    Message = $"Link to reset password sent successfully to email {user.Email}",
                    Token = token,
                    Email=email
                };

                return StatusCode(StatusCodes.Status200OK, response);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("Resetpassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return Ok(new
            {
                model
            });

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user != null)
            {
                var resetpasswordResult = await _userManager.ResetPasswordAsync(user,
                    resetPasswordModel.Token, resetPasswordModel.Password);

                if (!resetpasswordResult.Succeeded)
                {
                    foreach (var error in resetpasswordResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }

                return StatusCode(StatusCodes.Status200OK,

                        new Response { Status = "success", Message = $"Password Changed for email {user.Email}" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Password didn't change server error");
        }
    }
}
