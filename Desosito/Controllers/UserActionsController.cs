using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desosito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserActionService _userActionService;

        public UserActionsController(IPostService postService, IPostCommentService postCommentService, IUserProfileService userProfileService, IUserActionService userActionService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _userProfileService = userProfileService;
            _userActionService = userActionService;
        }


        [Authorize]
        [HttpPost("LikePost")]
        public async Task<IActionResult> LikePost(Guid id)
        {
            var result = await _userActionService.ActionLikePost(id, User.Identity.Name);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Description);
        }


        [Authorize]
        [HttpPost("GetUserLikePost")]
        public async Task<IActionResult> GetUserLikePost(int page)
        {
            var result = _userActionService.GetUserLikePost(User.Identity.Name, page);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Description);
        }






    }
}
