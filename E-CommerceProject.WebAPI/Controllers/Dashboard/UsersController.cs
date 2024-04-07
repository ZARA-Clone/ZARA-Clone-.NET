using E_CommerceProject.Business.Users.Dtos;
using E_CommerceProject.Business.Users.Interfaces;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers.Dashboard
{
    [Authorize(Roles = "Admin")]
    [Route("dashboard/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserService userService
            , ILogger<UsersController> logger
            , UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            if (id == null)
            {
                _logger.LogError($"Invalid id {id}");
                return NotFound();
            }
            var item = await _userService.GetById(id);
            if (item == null)
            {
                _logger.LogWarning($"There is no user  with id: {id}");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            _logger.LogInformation($"Get all products");
            var items = await _userService.GetAll();
            return items;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            _logger.LogInformation($"Deleting product with id {id}");
            try
            {
                var response = await _userService.Delete(id);
                if (response.IsSuccess)
                {
                    _logger.LogWarning($"User with id {id} has been deleted");
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"Deleting user with id {id} has been failed with errors{response.Errors}");
                    foreach (var error in response.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound();
            }
        }
    }
}
