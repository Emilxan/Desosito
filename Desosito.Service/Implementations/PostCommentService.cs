using Desosito.DAL.Interface;
using Desosito.DAL.Repositories;
using Desosito.Domain.Entity;
using Desosito.Domain.Enum;
using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Implementations
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IBaseRepository<PostComment> _postCommentRepository;

        public PostCommentService(IBaseRepository<PostComment> postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }

        public async Task<IBaseResponse<PostComment>> CreateCommentByPostId(Guid postId, string userId, PostCommentVM postCommentVM)
        {
            try
            {
                var post = new PostComment()
                {
                    UserName = userId,
                    Body = postCommentVM.Body,
                    PostId = postId,
                    CreateDateTime = DateTime.Now,
                };
                await _postCommentRepository.Create(post);


                return new BaseResponse<PostComment>()
                {
                    StatusCode = StatusCode.OK,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostComment>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public Task<IBaseResponse<PostComment>> EditCommentById(Guid id, PostVM model)
        {
            throw new NotImplementedException();
        }

        public IBaseResponse<List<PostComment>> GetAllCommentByPostId(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<PostCommentVM>> GetCommentByComId(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}
