using Desosito.Domain.ViewModel;
using Desosito.Service.Interfaces;
using Desosito.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desosito.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;
        private IAuthorizeService _authorizeService;
        private IUserProfileService _userProfileService;

        public AuthorizeController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IOptions<JWTSettings> options,
            IAuthorizeService authorizeService,
            IUserProfileService userProfileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options.Value;
            _authorizeService = authorizeService;
            _userProfileService = userProfileService;
        }
        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
        {
            var claims = prinicpal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audienct,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(3)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        /*
                [HttpPost("Register")]
                public async Task<IActionResult> Register(OtherParamUser paramUser)
                {
                    var user = new IdentityUser { UserName = paramUser.UserName, Email = paramUser.Email};

                    var result = await _userManager.CreateAsync(user, paramUser.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Role", paramUser.Role.ToString()));
                        claims.Add(new Claim(ClaimTypes.Email, paramUser.Email));

                        await _userManager.AddClaimsAsync(user, claims);
                    }
                    else
                    {
                        return BadRequest();
                    }

                    return Ok();
                }


                private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal) 
                {
                    var claims = prinicpal.ToList();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));

                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

                    var jwt = new JwtSecurityToken(
                        issuer: _options.Issuer,
                        audience: _options.Audienct,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(3)),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                        );

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                }


                [HttpPost("SingIn")]
                public async Task<IActionResult> SingIn(ParamUser paramUser)
                {
                    var user = await _userManager.FindByEmailAsync(paramUser.Email);

                    var result = await _signInManager.PasswordSignInAsync(user, paramUser.Password, false, false);

                    if (result.Succeeded)
                    {
                        IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                        var token = GetToken(user, claims);

                        return Ok(token);
                    }

                    return BadRequest();
                }*/

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are not valid");

            if (model == null) 
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword) 
                return BadRequest("Confirm password doesn't match the password");

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);




            Guid guidId = new Guid(identityUser.Id);
            await _userProfileService.CreateProfile(guidId, identityUser.UserName);



            if (result.Succeeded)
            {
                return Ok(result);
            }


            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Some properties are not valid");

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null) 
                return BadRequest("User not found!");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (result)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                return Ok(token);
            }

            return BadRequest("Invalid password");
        }



        private async Task<Guid> TakeCreateUserId(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            Guid result = new Guid(user.Id);

            return result;
        }
    }
}
