using System.Security.Claims;
using KidSpot.Application.Common;
using KidSpot.Application.Reviews.Commands;
using KidSpot.Application.Reviews.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidSpot.API.Controllers;

[ApiController]
[Route("api/places/{placeId:guid}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly GetReviewsByPlaceHandler _getReviewsHandler;
    private readonly AddReviewHandler _addReviewHandler;

    public ReviewsController(
        GetReviewsByPlaceHandler getReviewsHandler,
        AddReviewHandler addReviewHandler)
    {
        _getReviewsHandler = getReviewsHandler;
        _addReviewHandler = addReviewHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetByPlace(Guid placeId, CancellationToken cancellationToken)
    {
        var result = await _getReviewsHandler.HandleAsync(placeId, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(
        Guid placeId,
        [FromBody] AddReviewCommand command,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
            return Unauthorized(ApiResponse<object>.Failure("UNAUTHORIZED", "User not authenticated."));

        var result = await _addReviewHandler.HandleAsync(placeId, userId.Value, command, cancellationToken);
        if (result.Error is not null)
        {
            return result.Error.Code switch
            {
                "NOT_FOUND" => NotFound(result),
                "VALIDATION_ERROR" => BadRequest(result),
                _ => BadRequest(result)
            };
        }

        return Created($"/api/places/{placeId}/reviews", result);
    }

    private Guid? GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(claim, out var id) ? id : null;
    }
}
