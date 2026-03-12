using System.Security.Claims;
using KidSpot.Application.Common;
using KidSpot.Application.Users.Commands;
using KidSpot.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidSpot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPlaceRepository _placeRepository;

    public UsersController(IUserRepository userRepository, IPlaceRepository placeRepository)
    {
        _userRepository = userRepository;
        _placeRepository = placeRepository;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
            return Unauthorized(ApiResponse<object>.Failure("UNAUTHORIZED", "User not authenticated."));

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", "User not found."));

        var userDto = new UserDto(user.Id, user.Email, user.Name, user.AvatarUrl, user.IsAdmin);
        return Ok(ApiResponse<UserDto>.Success(userDto));
    }

    [HttpPost("me/saved-places/{placeId:guid}")]
    public async Task<IActionResult> SavePlace(Guid placeId, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
            return Unauthorized(ApiResponse<object>.Failure("UNAUTHORIZED", "User not authenticated."));

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", "User not found."));

        var place = await _placeRepository.GetByIdAsync(placeId, cancellationToken);
        if (place is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", $"Place with id '{placeId}' was not found."));

        user.SavePlace(placeId);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Ok(ApiResponse<object>.Success(new { message = "Place saved." }));
    }

    [HttpDelete("me/saved-places/{placeId:guid}")]
    public async Task<IActionResult> RemoveSavedPlace(Guid placeId, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
            return Unauthorized(ApiResponse<object>.Failure("UNAUTHORIZED", "User not authenticated."));

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", "User not found."));

        user.RemoveSavedPlace(placeId);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Ok(ApiResponse<object>.Success(new { message = "Place removed from saved." }));
    }

    private Guid? GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(claim, out var id) ? id : null;
    }
}
