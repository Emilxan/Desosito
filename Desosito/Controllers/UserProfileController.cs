using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desosito.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {

        private readonly IUserProfileService _userProfileService;
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(Guid id)
        {
            var result = await _userProfileService.GetProfileById(id);

            if(result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost("EditUserProfile")]
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileVM model, Guid id)
        {
            var result = await _userProfileService.EditProfile(id, model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }




    }
}
