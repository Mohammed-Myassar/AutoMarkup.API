using Application.Abstractions;
using Application.ViewModel.UsersViewModel;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarkup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(
            IUserService userService,
            ILogger<UsersController> logger
            )
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("rigester")]
        public async Task<ActionResult<UserDto>> CreateAccount(RegisterRequest request)
        {
            var result = await userService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAccount(LoginRequest request)
        {
            var result = await userService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPut("update-info-profile/{userId:guid}")]
        public async Task<ActionResult<UserDto>> UpdateProfile(Guid userId,
        JsonPatchDocument<UserProfileDto> document)
        {
            var result = await userService.UpdateProfileAsync(userId, document);
            return Ok(result);
        }

        [HttpPut("change-password/{userId:guid}")]
        public async Task<ActionResult<bool>> ChangePasswordAsync(Guid userId,
            ChangePasswordRequest request)
        {
            var result = await userService.ChangePasswordAsync(userId, request);
            return Ok(result);
        }

        [HttpGet("get-user-profile/{userId:guid}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(Guid userId)
        {
            var result = await userService.GetUserProfileAsync(userId);
            return Ok(result);
        }
    }
}
