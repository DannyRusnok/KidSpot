using KidSpot.Application.Common;
using KidSpot.Domain.Repositories;
using KidSpot.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace KidSpot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceRepository _placeRepository;

    public PlacesController(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
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
        return Ok(ApiResponse<object>.Success(places));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var place = await _placeRepository.GetByIdAsync(id, cancellationToken);
        if (place is null)
            return NotFound(ApiResponse<object>.Failure("NOT_FOUND", $"Place with id '{id}' was not found."));

        return Ok(ApiResponse<object>.Success(place));
    }
}
