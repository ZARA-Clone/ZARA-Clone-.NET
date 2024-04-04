using E_CommerceProject.Business.Contactus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_CommerceProject.Business.Contactus.Dto;
namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {

        private readonly ContactUs _Contactus;
        public ContactUsController(ContactUs contactus)
        {
            _Contactus = contactus;
        }


        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromBody] ContactusDto contactDto)
        {
            try {


                await _Contactus.SendEmailAsync(contactDto.ToEmail, contactDto.Subject, contactDto.Body);
                return Ok();

            }
            catch { 
            return BadRequest();
            }
        }


    }
}
