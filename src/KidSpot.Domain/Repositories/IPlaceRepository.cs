using KidSpot.Domain.Entities;
using KidSpot.Domain.ValueObjects;

namespace KidSpot.Domain.Repositories;

public interface IPlaceRepository
{
    Task<Place?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Place>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Place>> GetByCityAsync(string city, PlaceType? type, int? ageFrom, int? ageTo, CancellationToken cancellationToken = default);
    Task AddAsync(Place place, CancellationToken cancellationToken = default);
    Task UpdateAsync(Place place, CancellationToken cancellationToken = default);
}
