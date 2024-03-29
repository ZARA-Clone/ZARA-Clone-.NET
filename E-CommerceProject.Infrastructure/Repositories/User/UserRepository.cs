//using E_CommerceProject.Models.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.Data;
//using System.Threading.Tasks;


//namespace E_CommerceProject.Infrastructure.Repositories.User
//{
//    public class UserRepository 
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public UserRepository(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<bool> CreateUserAsync(RegReq newUser)
//        {
//            ApplicationUser user = new ApplicationUser
//            {
//                Email = newUser.Email,
//                PhoneNumber = newUser.PhoneNumber,
//                UserName = newUser.UserName,
//                Country = newUser.Country
//            };

//            var result = await _userManager.CreateAsync(user, newUser.Password);

//            return result.Succeeded;
//        }

      
//    }
//}

