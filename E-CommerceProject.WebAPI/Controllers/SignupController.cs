﻿using E_CommerceProject.Business.user.Dtos;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models.Models;
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
    public class SignupController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ECommerceContext _context;
        private readonly IConfiguration _configuration;

        public SignupController(UserManager<ApplicationUser> userManager
            , ECommerceContext context
            , IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("checkEmailExists")]
        public IActionResult CheckEmailExists([FromBody] RegReq userData)
        {
            if (userData == null || string.IsNullOrWhiteSpace(userData.Email))
            {
                return BadRequest("Invalid user data or email is missing.");
            }

            var userWithEmail = _context.Users.FirstOrDefault(u => u.Email == userData.Email || u.UserName == userData.UserName);
            return Ok(userWithEmail != null);
        }





        [HttpPost("checkNameExists")]
        public IActionResult CheckNameExists([FromBody] RegReq userData)
        {
            if (userData == null || string.IsNullOrWhiteSpace(userData.UserName))
            {
                return BadRequest("Invalid user data or name is missing.");
            }

            var userWithName = _context.Users.FirstOrDefault(u => u.UserName == userData.UserName);
            return Ok(userWithName != null);
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
            user.UserName = newuser.UserName;
            user.Country = newuser.country;
            user.Address = newuser.Address;


            var result = await _userManager.CreateAsync(user, newuser.Password);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);

                return Ok(new { Token = token });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                 new Claim("uid", user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
                 new Claim(ClaimTypes.Role, "customer"),
                 new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            //secret key
            string Key = "your_secret_key_herebsdgghsghbqgugs";

            var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

            //create token 
            var signer = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            //generate token  //object
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signer,
                expires: DateTime.Now.AddDays(5));


            //encoding it
            var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return stringtoken;
        }


    }
}
