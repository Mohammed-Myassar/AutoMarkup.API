using Application.Abstractions;
using Application.ViewModel.UsersViewModel;
using AutoMapper;
using Domain.AccountEntity;
using Infrastructure.Interface;
using Microsoft.AspNetCore.JsonPatch;

public class UserService : IUserService
{
    private readonly IAccountRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IAccountRepository userRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser != null)
            throw new ArgumentException("User already exists with this email");

        var user = _mapper.Map<User>(request);
        user.PasswordHash = _passwordHasher.HashPassword(request.Password);

        var createdUser = await _userRepository.AddAsync(user);

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail);

        if (user == null || !_passwordHasher
            .VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserProfileDto> UpdateProfileAsync(Guid userId,
        JsonPatchDocument<UserProfileDto> document)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found");

        var userProfileDto = _mapper.Map<UserProfileDto>(user);
        document.ApplyTo(userProfileDto);

        user.FirstName = userProfileDto.FirstName;
        user.LastName = userProfileDto.LastName;
        user.Email = userProfileDto.Email;

        var updatedUser = await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserProfileDto>(updatedUser);
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found");

        if (!_passwordHasher.VerifyPassword(request.OldPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Current password is incorrect");

        user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdWithProjectsAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found");

        return _mapper.Map<UserProfileDto>(user);
    }
}