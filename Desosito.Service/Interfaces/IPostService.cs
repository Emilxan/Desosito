using Desosito.Domain.Entity;
using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Interfaces
{
    public interface IPostService
    {

        Task<IBaseResponse<PostVM>> GetPostById(Guid id);

        Task<IBaseResponse<Post>> EditPostById(Guid id, EditPostVM model);

        Task<IBaseResponse<Post>> CreatePost(string createPostUserName, CreatePostVM postVM);

        Task<IBaseResponse<Post>> ReturnUserNameByPost(Guid id);

        Task<IBaseResponse<bool>> DeletePost(Guid id);

        IBaseResponse<List<Post>> GetLentPost();

    }
}
