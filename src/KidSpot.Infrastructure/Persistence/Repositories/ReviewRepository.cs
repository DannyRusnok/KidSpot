using KidSpot.Domain.Entities;
using KidSpot.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KidSpot.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly KidSpotDbContext _context;

    public ReviewRepository(KidSpotDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Review>> GetByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(r => r.PlaceId == placeId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
    {
        await _context.Reviews.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
