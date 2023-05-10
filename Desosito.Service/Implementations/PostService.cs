using Desosito.DAL.Interface;
using Desosito.DAL.Repositories;
using Desosito.Domain.Entity;
using Desosito.Domain.Enum;
using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly IBaseRepository<Post> _postRepository;
        private readonly IUserProfileService _userProfileService;

        public PostService(IBaseRepository<Post> postRepository, IUserProfileService userProfileService)
        {
            _postRepository = postRepository;
            _userProfileService = userProfileService;
        }


        public async Task<IBaseResponse<Post>> CreatePost(string createPostUserName, CreatePostVM postVM)
        {
            try
            {
                var post = new Post()
                {
                    UserName = createPostUserName,
                    Body = postVM.Body,
                    CreateDateTime = DateTime.Now,
                    CommentScore = 0,
                    LikeScore = 0,
                    RepostScore = 0,
                };
                await _postRepository.Create(post);

                return new BaseResponse<Post>()
                {
                    StatusCode = StatusCode.OK,
                    Data = post
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePost(Guid id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound,
                        Data = false
                    };
                }

                post.DeleteDateTime = DateTime.Now;

                await _postRepository.Update(post);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> EditPostById(Guid id, EditPostVM model)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                
                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                post.Body = model.Body;
                post.EditDateTime = DateTime.Now;

                await _postRepository.Update(post);

                return new BaseResponse<Post>()
                {
                    Data = post,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Post>> GetLentPost()
        {
            try
            {
                var cars = _postRepository.GetAll().ToList();


                if (!cars.Any())
                {
                    return new BaseResponse<List<Post>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Post>>()
                {
                    Data = cars,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Post>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PostVM>> GetPostById(Guid id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                {
                    return new BaseResponse<PostVM>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PostVM()
                {
                    Body = post.Body,
                    CreateDateTime = post.CreateDateTime,
                    UserName = post.UserName,
                    RepostScore = post.RepostScore,
                    LikeScore = post.LikeScore,
                    CommentScore = post.CommentScore,
                    EditDateTime = post.EditDateTime,
                    DeleteDateTime = post.DeleteDateTime,
                };






                return new BaseResponse<PostVM>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostVM>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Post>> ReturnUserNameByPost(Guid id)
        {
            try
            {
                var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (post == null)
                {
                    return new BaseResponse<Post>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                return new BaseResponse<Post>()
                {
                    Data = post,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Post>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
