using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Desosito.Service.Implementations;
using Desosito.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Desosito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        private readonly IUserProfileService _userProfileService;

        public PostController(IPostService postService, IPostCommentService postCommentService, IUserProfileService userProfileService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _userProfileService = userProfileService;
        }


        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            
            var result = await _postService.GetPostById(id);
            var user = await _userProfileService.GetProfileByUserName(result.Data.UserName);
            result.Data.UserProfile = user.Data;

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }


        [Authorize]
        [HttpPost("EditPostById")]
        public async Task<IActionResult> EditPostById([FromBody] EditPostVM model, Guid id)
        {
            var userName = _postService.ReturnUserNameByPost(id);
            if (User.Identity.Name != userName.Result.Data.UserName)
            {
                return BadRequest();
            }
            var result = await _postService.EditPostById(id, model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }


        [Authorize]
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostVM model)
        {
            var result = await _postService.CreatePost(User.Identity.Name, model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }


        [Authorize]
        [HttpPost("CreatePostComment")]
        public async Task<IActionResult> CreatePostComment([FromBody] PostCommentVM model, Guid postId)
        {
            var result = await _postCommentService.CreateCommentByPostId(postId, User.Identity.Name, model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }


        [Authorize]
        [HttpPost("DeletePostById")]
        public async Task<IActionResult> DeletePostById(Guid id)
        {
            var userName = _postService.ReturnUserNameByPost(id);
            if (User.Identity.Name != userName.Result.Data.UserName)
            {
                return BadRequest();
            }
            var result = await _postService.DeletePost(id);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
    }
}
