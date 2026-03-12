using System.Security.Claims;
using KidSpot.Application.Common;
using KidSpot.Application.Places.Commands;
using KidSpot.Domain.Repositories;
using KidSpot.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidSpot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceRepository _placeRepository;
    private readonly CreatePlaceHandler _createHandler;
    private readonly UpdatePlaceHandler _updateHandler;

    public PlacesController(
        IPlaceRepository placeRepository,
        CreatePlaceHandler createHandler,
        UpdatePlaceHandler updateHandler)
    {
        _placeRepository = placeRepository;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetByCity(
        [FromQuery] string city,
        [FromQuery] int? ageFrom,
        [FromQuery] int? ageTo,
        [FromQuery] PlaceType? type,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest(ApiResponse<object>.Failure("VALIDATION_ERROR", "City parameter is required."));

        var places = await _placeRepository.GetByCityAsync(city, type, ageFrom, ageTo, cancellationToken);
        var dtos = places.Select(CreatePlaceHandler.MapToDto).ToList();
        return Ok(ApiResponse<List<PlaceDto>>.Success(dtos));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var place = await _placeRepository.GetByIdAsync(id, cancellationToken);
        if (place is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", $"Place with id '{id}' was not found."));

        return Ok(ApiResponse<PlaceDto>.Success(CreatePlaceHandler.MapToDto(place)));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(
        [FromBody] CreatePlaceCommand command,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
            return Unauthorized(ApiResponse<object>.Failure("UNAUTHORIZED", "User not authenticated."));

        var result = await _createHandler.HandleAsync(command, userId.Value, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdatePlaceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _updateHandler.HandleAsync(id, command, cancellationToken);
        if (result.Error is not null)
            return NotFound(result);

        return Ok(result);
    }

    private Guid? GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(claim, out var id) ? id : null;
    }
}
