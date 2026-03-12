using KidSpot.Application.Common;
using KidSpot.Domain.Entities;
using KidSpot.Domain.Repositories;
using KidSpot.Domain.ValueObjects;

namespace KidSpot.Application.Places.Commands;

public record CreatePlaceCommand(
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

public record PlaceDto(
    Guid Id,
    string Name,
    string Description,
    string? GooglePlaceId,
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
    double AverageRating,
    DateTime CreatedAt
);

public class CreatePlaceHandler
{
    private readonly IPlaceRepository _placeRepository;

    public CreatePlaceHandler(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task<ApiResponse<PlaceDto>> HandleAsync(
        CreatePlaceCommand command,
        Guid curatedBy,
        CancellationToken cancellationToken = default)
    {
        var location = new Location(command.Latitude, command.Longitude, command.Address, command.City, command.Country);
        var kidsAttributes = new KidsAttributes(command.ChangingTable, command.KidsMenu, command.StrollerFriendly, command.AgeFrom, command.AgeTo);

        var place = Place.Create(
            command.Name,
            command.Description,
            location,
            command.Type,
            kidsAttributes,
            curatedBy,
            command.GooglePlaceId);

        await _placeRepository.AddAsync(place, cancellationToken);

        return ApiResponse<PlaceDto>.Success(MapToDto(place));
    }

    public static PlaceDto MapToDto(Place place) => new(
        place.Id,
        place.Name,
        place.Description,
        place.GooglePlaceId,
        place.Location.Latitude,
        place.Location.Longitude,
        place.Location.Address,
        place.Location.City,
        place.Location.Country,
        place.Type,
        place.KidsAttributes.ChangingTable,
        place.KidsAttributes.KidsMenu,
        place.KidsAttributes.StrollerFriendly,
        place.KidsAttributes.AgeFrom,
        place.KidsAttributes.AgeTo,
        place.AverageRating,
        place.CreatedAt
    );
}
