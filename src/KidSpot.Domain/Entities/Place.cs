using KidSpot.Domain.ValueObjects;

namespace KidSpot.Domain.Entities;

public class Place
{
    private readonly List<Review> _reviews = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string? GooglePlaceId { get; private set; }
    public Location Location { get; private set; } = null!;
    public PlaceType Type { get; private set; }
    public KidsAttributes KidsAttributes { get; private set; } = null!;
    public Guid CuratedBy { get; private set; }
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();
    public double AverageRating { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Place() { } // EF Core constructor

    public static Place Create(
        string name,
        string description,
        Location location,
        PlaceType type,
        KidsAttributes kidsAttributes,
        Guid curatedBy,
        string? googlePlaceId = null)
    {
        return new Place
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Location = location,
            Type = type,
            KidsAttributes = kidsAttributes,
            CuratedBy = curatedBy,
            GooglePlaceId = googlePlaceId,
            AverageRating = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string name,
        string description,
        Location location,
        PlaceType type,
        KidsAttributes kidsAttributes,
        string? googlePlaceId = null)
    {
        Name = name;
        Description = description;
        Location = location;
        Type = type;
        KidsAttributes = kidsAttributes;
        GooglePlaceId = googlePlaceId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecalculateAverageRating()
    {
        AverageRating = _reviews.Count > 0
            ? _reviews.Average(r => r.Rating)
            : 0;
        UpdatedAt = DateTime.UtcNow;
    }
}
