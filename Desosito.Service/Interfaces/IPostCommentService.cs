using Desosito.Domain.Entity;
using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Interfaces
{
    public interface IPostCommentService
    {
        IBaseResponse<List<PostComment>> GetAllCommentByPostId(Guid postId);

        Task<IBaseResponse<PostCommentVM>> GetCommentByComId(Guid commentId);

        Task<IBaseResponse<PostComment>> EditCommentById(Guid id, PostVM model);

        Task<IBaseResponse<PostComment>> CreateCommentByPostId(Guid postId, string userId, PostCommentVM postCommentVM);
    }
}
