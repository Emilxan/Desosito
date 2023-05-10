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
    public interface IUserProfileService
    {
        IBaseResponse<List<UserProfile>> GetMyProfile();

        Task<IBaseResponse<UserProfileVM>> GetProfileById(Guid id);
        Task<IBaseResponse<UserProfileVM>> GetProfileByUserName(string userName);

        Task<IBaseResponse<UserProfile>> EditProfile(Guid id, UserProfileVM model);

        Task<IBaseResponse<UserProfile>> CreateProfile(Guid createUserId, string userName);
    }
}
