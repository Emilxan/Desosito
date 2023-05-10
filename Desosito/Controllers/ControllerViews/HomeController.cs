using Desosito.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desosito.Controllers.ControllerViews
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        private readonly IUserProfileService _userProfileService;

        public HomeController(IPostService postService, IPostCommentService postCommentService, IUserProfileService userProfileService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _userProfileService = userProfileService;
        }


/*        [HttpGet("GetPostLent")]
        public async Task<IActionResult> GetPostLent()
        {
            var result = await _postService.GetLentPost();
            //var user = await _userProfileService.GetProfile(result);
            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }*/
    }
}
