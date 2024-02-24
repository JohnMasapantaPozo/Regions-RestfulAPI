using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulDEMO.API.Models.Dtos;
using RestfulDEMO.API.Repositories;

namespace RestfulDEMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository
            )
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        // POST: /apiAuth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    // Add roles to user and send to Auth DB
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was regitered. Please log in.");
                    }
                }
            }
            return BadRequest("Something wnet wrong");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Check roles for the user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create and return Jwt token: TokenRepository:ITokenRepository
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                        return Ok(new LoginResponseDto{JwtToken = jwtToken});
                    }
                };
            }
            return BadRequest("Username or Password incorrect");
        }
    }
}
