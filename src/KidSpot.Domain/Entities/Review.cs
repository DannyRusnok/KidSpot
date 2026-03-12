namespace KidSpot.Domain.Entities;

public class Review
{
    public Guid Id { get; private set; }
    public Guid PlaceId { get; private set; }
    public Guid UserId { get; private set; }
    public int Rating { get; private set; }
    public string Text { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public Place Place { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private Review() { } // EF Core constructor

    public static Review Create(Guid placeId, Guid userId, int rating, string text)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Review text cannot be empty.", nameof(text));

        return new Review
        {
            Id = Guid.NewGuid(),
            PlaceId = placeId,
            UserId = userId,
            Rating = rating,
            Text = text,
            CreatedAt = DateTime.UtcNow
        };
    }
}
