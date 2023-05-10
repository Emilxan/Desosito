using Desosito.Domain.Responce;
using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Desosito.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private IUserProfileService _userProfileService;

        public AuthorizeService(UserManager<IdentityUser> userManager, IConfiguration configuration, IUserProfileService userProfileService)
        {
            _userManger = userManager;
            _configuration = configuration;
            _userProfileService = userProfileService;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginVM model)
        {
            var user = await _userManger.FindByNameAsync(model.UserName);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:Issuer"],
                audience: _configuration["JWTSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterVM model)
        {
            if (model == null) throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);


            var guidId = TakeCreateUserId(model.UserName);
            await _userProfileService.CreateProfile(guidId.Result , model.UserName);


            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        private async Task<Guid> TakeCreateUserId(string UserName)
        {
            var user = await _userManger.FindByNameAsync(UserName);

            Guid result = new Guid(user.Id);
            
            return result;
        }
    }
    public interface IAuthorizeService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterVM model);

        Task<UserManagerResponse> LoginUserAsync(LoginVM model);
    }
}
