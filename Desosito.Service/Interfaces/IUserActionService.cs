using Desosito.Domain.Entity;
using Desosito.Domain.Entity.UserAction;
using Desosito.Domain.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Interfaces
{
    public interface IUserActionService
    {
        // Repost
        //Task<IBaseResponse<int>> ActionRepostPost(Guid postId, string userName);

        //IBaseResponse<List<RepostPost>> GetUserRepostPost(string userName, int page = 0);


        // Like 



        Task<IBaseResponse<int>> ActionLikePost(Guid postId, string userName);

        IBaseResponse<List<LikePost>> GetUserLikePost(string userName, int page = 0);
    }
}
