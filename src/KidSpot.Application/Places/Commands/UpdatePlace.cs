using KidSpot.Application.Common;
using KidSpot.Domain.Repositories;
using KidSpot.Domain.ValueObjects;

namespace KidSpot.Application.Places.Commands;

public record UpdatePlaceCommand(
    string Name,
    string Description,
    double Latitude,
    double Longitude,
    string Address,
    string City,
    string Country,
    PlaceType Type,
    bool ChangingTable,
    bool KidsMenu,
    bool StrollerFriendly,
    int AgeFrom,
    int AgeTo,
    string? GooglePlaceId
);

public class UpdatePlaceHandler
{
    private readonly IPlaceRepository _placeRepository;

    public UpdatePlaceHandler(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task<ApiResponse<PlaceDto>> HandleAsync(
        Guid placeId,
        UpdatePlaceCommand command,
        CancellationToken cancellationToken = default)
    {
        var place = await _placeRepository.GetByIdAsync(placeId, cancellationToken);
        if (place is null)
            return ApiResponse<PlaceDto>.Failure("NOT_FOUND", $"Place with id '{placeId}' was not found.");

        var location = new Location(command.Latitude, command.Longitude, command.Address, command.City, command.Country);
        var kidsAttributes = new KidsAttributes(command.ChangingTable, command.KidsMenu, command.StrollerFriendly, command.AgeFrom, command.AgeTo);

        place.Update(command.Name, command.Description, location, command.Type, kidsAttributes, command.GooglePlaceId);
        await _placeRepository.UpdateAsync(place, cancellationToken);

        return ApiResponse<PlaceDto>.Success(CreatePlaceHandler.MapToDto(place));
    }
}
