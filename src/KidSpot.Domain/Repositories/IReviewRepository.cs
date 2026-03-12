using KidSpot.Domain.Entities;

namespace KidSpot.Domain.Repositories;

public interface IReviewRepository
{
    Task<IReadOnlyList<Review>> GetByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken = default);
    Task AddAsync(Review review, CancellationToken cancellationToken = default);
}
