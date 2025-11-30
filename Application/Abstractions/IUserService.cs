using Application.ViewModel.UsersViewModel;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterRequest request);
        Task<UserDto> LoginAsync(LoginRequest request);
        Task<UserProfileDto> UpdateProfileAsync(Guid userId,
            JsonPatchDocument<UserProfileDto> document);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest newPassword);
        Task<UserProfileDto> GetUserProfileAsync(Guid userId);
    }
}
