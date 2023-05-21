using Desosito.DAL.Interface;
using Desosito.Domain.Entity.UserAction;
using Desosito.Domain.Entity;
using Desosito.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desosito.Domain.Responce;
using Desosito.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Desosito.Service.Implementations
{
    public class UserActionService : IUserActionService
    {
        private readonly IBaseRepository<Post> _postRepository;
        private readonly IUserProfileService _userProfileService;  
        private readonly IBaseRepository<LikePost> _likePostRepository;
        //private readonly IBaseRepository<RepostPost> _repostPostRepository;

        public UserActionService(IBaseRepository<Post> postRepository, 
            IUserProfileService userProfileService, 
            IBaseRepository<LikePost> likePostRepository
            )
        {
            _postRepository = postRepository;
            _userProfileService = userProfileService;
            _likePostRepository = likePostRepository;

        }

        // Repost

        /*        public async Task<IBaseResponse<int>> ActionRepostPost(Guid postId, string userName)
                {
                    try
                    {
                        var repostPostSearch = await _repostPostRepository.GetAll().FirstOrDefaultAsync(x => x.PostId == postId && x.UserName == userName);

                        if (repostPostSearch != null)
                        {
                            return new BaseResponse<int>()
                            {
                                Data = 999,
                                StatusCode = StatusCode.OK
                            };
                        }

                        var repostPost = new RepostPost()
                        {
                            UserName = userName,
                            PostId = postId,
                            CreateDatetime = DateTime.Now,
                        };
                        await _repostPostRepository.Create(repostPost);

                        var repostScore = LikeScore(postId).Result.Data;
                        await EditPostScore(postId, repostScore, 0);

                        return new BaseResponse<int>()
                        {
                            Data = repostScore,
                            StatusCode = StatusCode.OK
                        };
                    }
                    catch (Exception ex)
                    {
                        ;
                        return new BaseResponse<int>()
                        {
                            Description = $"[DeleteCar] : {ex.Message}",
                            StatusCode = StatusCode.InternalServerError
                        };
                    }
                }

                public IBaseResponse<List<RepostPost>> GetUserRepostPost(string userName, int page = 0)
                {
                    try
                    {
                        var cars = _repostPostRepository.GetAll().Where(x => x.UserName == userName).ToList();

                        if (!cars.Any())
                        {
                            return new BaseResponse<List<RepostPost>>()
                            {
                                Description = "Найдено 0 элементов",
                                StatusCode = StatusCode.OK
                            };
                        }

                        return new BaseResponse<List<RepostPost>>()
                        {
                            Data = cars,
                            StatusCode = StatusCode.OK
                        };
                    }
                    catch (Exception ex)
                    {
                        return new BaseResponse<List<RepostPost>>()
                        {
                            Description = $"[GetCars] : {ex.Message}",
                            StatusCode = StatusCode.InternalServerError
                        };
                    }
                }

        */


        // Like 
        public async Task<IBaseResponse<int>> ActionLikePost(Guid postId, string userName)
        {
            try
            {
                var likePostSearch = await _likePostRepository.GetAll().FirstOrDefaultAsync(x => x.PostId == postId && x.UserName == userName);

                if (likePostSearch != null)
                {
                    return new BaseResponse<int>()
                    {
                        Data = 999,
                        StatusCode = StatusCode.OK
                    };
                }

                var likePost = new LikePost()
                {
                    UserName = userName,
                    PostId = postId,
                    CreateDatetime = DateTime.Now,
                };
                await _likePostRepository.Create(likePost);

                var likeScore = LikeScore(postId).Result.Data;
                await EditPostScore(postId, likeScore, 0);

                return new BaseResponse<int>()
                {
                    Data = likeScore,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                ;
                return new BaseResponse<int>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<LikePost>> GetUserLikePost(string userName, int page = 0)
        {
            try
            {
                var userLikePost = _likePostRepository.GetAll().Include(x => x.UserName == userName).Skip(page * 10).Take(10).ToList();

                if (!userLikePost.Any())
                {
                    return new BaseResponse<List<LikePost>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<LikePost>>()
                {
                    Data = userLikePost,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<LikePost>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        // PRIVATE

        private async Task<IBaseResponse<int>> LikeScore(Guid postId)
        {
            try
            {
                var likeScore = _likePostRepository.GetAll().Where(x => x.PostId == postId).ToList();

                return new BaseResponse<int>()
                {
                    Data = likeScore.Count(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private async Task<IBaseResponse<bool>> EditPostScore(Guid postId, int score, int whichState)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == postId);

                if (post == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                switch (whichState)
                {
                    case 0:
                        post.LikeScore = score;
                        break;
                    case 1:
                        post.CommentScore = score;
                        break;
                    case 2:
                        post.RepostScore = score;
                        break;
                }

                await _postRepository.Update(post);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


    }
}
