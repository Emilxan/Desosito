using Desosito.DAL.Interface;
using Desosito.Domain.Entity;
using Desosito.Domain.Enum;
using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository<UserProfile> _userProfileRepository;

        public UserProfileService(IBaseRepository<UserProfile> userProfielRepository)
        {
            _userProfileRepository = userProfielRepository;
        }

        public async Task<IBaseResponse<UserProfile>> CreateProfile(Guid createUserId, string userName)
        {
            try
            {
                var userProfile = new UserProfile()
                {
                    Id = createUserId,
                    UserName = userName,
                    FirstName = "",
                    SecondName = "",
                    StatusText = "",
                };
                await _userProfileRepository.Create(userProfile);


                return new BaseResponse<UserProfile>()
                {
                    StatusCode = StatusCode.OK,
                    Data = userProfile
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfile>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<UserProfile>> EditProfile(Guid id, UserProfileVM model)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (userProfile == null)
                {
                    return new BaseResponse<UserProfile>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                userProfile.FirstName = model.FirstName;
                userProfile.SecondName = model.SecondName;
                userProfile.StatusText = model.Status;

                await _userProfileRepository.Update(userProfile);


                return new BaseResponse<UserProfile>()
                {
                    Data = userProfile,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfile>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<UserProfile>> GetMyProfile()
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<UserProfileVM>> GetProfileById(Guid id)
        {
            try
            {
                var car = await _userProfileRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return new BaseResponse<UserProfileVM>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new UserProfileVM()
                {
                    FirstName = car.FirstName,
                    SecondName = car.SecondName,
                    Status = car.StatusText,
                };

                return new BaseResponse<UserProfileVM>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileVM>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<UserProfileVM>> GetProfileByUserName(string userName)
        {
            try
            {
                var car = await _userProfileRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);
                if (car == null)
                {
                    return new BaseResponse<UserProfileVM>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new UserProfileVM()
                {
                    FirstName = car.FirstName,
                    SecondName = car.SecondName,
                    Status = car.StatusText,
                };

                return new BaseResponse<UserProfileVM>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileVM>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
