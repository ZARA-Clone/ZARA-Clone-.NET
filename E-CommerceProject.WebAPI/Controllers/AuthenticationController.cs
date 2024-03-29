using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //Check if user exists in db
            var user = await _userManager.FindByEmailAsync(loginModel.UserEmail);

            //Check if password is correct
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userrole = await _userManager.GetRolesAsync(user);


                //claimlist creation
                var authenticationClaims = new List<Claim>
         {
             
             new Claim("uid",user.Id.ToString()),
             new Claim(ClaimTypes.Email,user.Email),
            
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
                    validfrom = jwtToken.ValidFrom,
                    expiration = jwtToken.ValidTo

                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authenticationClaims)
        {
            var authenticationSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var Token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authenticationClaims,
                    signingCredentials: new SigningCredentials(authenticationSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return Token;
        }
    }
}
