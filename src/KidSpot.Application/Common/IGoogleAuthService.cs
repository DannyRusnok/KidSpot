namespace KidSpot.Application.Common;

public interface IGoogleAuthService
{
    Task<GoogleUserInfo?> ValidateGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default);
}

public record GoogleUserInfo(
    string GoogleId,
    string Email,
    string Name,
    string? AvatarUrl
);
