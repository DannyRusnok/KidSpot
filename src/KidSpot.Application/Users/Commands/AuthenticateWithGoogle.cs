using KidSpot.Application.Common;
using KidSpot.Domain.Entities;
using KidSpot.Domain.Repositories;

namespace KidSpot.Application.Users.Commands;

public record AuthenticateWithGoogleCommand(string IdToken);

public record AuthResult(string Token, UserDto User);

public record UserDto(Guid Id, string Email, string Name, string? AvatarUrl, bool IsAdmin);

public class AuthenticateWithGoogleHandler
{
    private readonly IGoogleAuthService _googleAuth;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticateWithGoogleHandler(
        IGoogleAuthService googleAuth,
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _googleAuth = googleAuth;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ApiResponse<AuthResult>> HandleAsync(
        AuthenticateWithGoogleCommand command,
        CancellationToken cancellationToken = default)
    {
        var googleUser = await _googleAuth.ValidateGoogleTokenAsync(command.IdToken, cancellationToken);
        if (googleUser is null)
            return ApiResponse<AuthResult>.Failure("INVALID_TOKEN", "Google token is invalid or expired.");

        var user = await _userRepository.GetByGoogleIdAsync(googleUser.GoogleId, cancellationToken);

        if (user is null)
        {
            user = User.Create(googleUser.Email, googleUser.Name, googleUser.GoogleId, googleUser.AvatarUrl);
            await _userRepository.AddAsync(user, cancellationToken);
        }
        else
        {
            user.UpdateProfile(googleUser.Name, googleUser.AvatarUrl);
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        var token = _jwtTokenService.GenerateToken(user);
        var userDto = new UserDto(user.Id, user.Email, user.Name, user.AvatarUrl, user.IsAdmin);

        return ApiResponse<AuthResult>.Success(new AuthResult(token, userDto));
    }
}
