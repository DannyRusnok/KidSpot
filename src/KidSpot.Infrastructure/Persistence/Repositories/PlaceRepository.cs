using KidSpot.Domain.Entities;
using KidSpot.Domain.Repositories;
using KidSpot.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KidSpot.Infrastructure.Persistence.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly KidSpotDbContext _context;

    public PlaceRepository(KidSpotDbContext context)
    {
        _context = context;
    }

    public async Task<Place?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Places
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Place>> GetByCityAsync(
        string city,
        PlaceType? type,
        int? ageFrom,
        int? ageTo,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Places
            .Where(p => p.Location.City.ToLower() == city.ToLower());

        if (type.HasValue)
            query = query.Where(p => p.Type == type.Value);

        if (ageFrom.HasValue)
            query = query.Where(p => p.KidsAttributes.AgeTo >= ageFrom.Value);

        if (ageTo.HasValue)
            query = query.Where(p => p.KidsAttributes.AgeFrom <= ageTo.Value);

        return await query
            .OrderByDescending(p => p.AverageRating)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Place place, CancellationToken cancellationToken = default)
    {
        await _context.Places.AddAsync(place, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Place place, CancellationToken cancellationToken = default)
    {
        _context.Places.Update(place);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
