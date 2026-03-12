namespace KidSpot.Domain.Entities;

public class SavedPlace
{
    public Guid UserId { get; private set; }
    public Guid PlaceId { get; private set; }
    public DateTime SavedAt { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Place Place { get; private set; } = null!;

    private SavedPlace() { } // EF Core constructor

    public SavedPlace(Guid userId, Guid placeId, DateTime savedAt)
    {
        UserId = userId;
        PlaceId = placeId;
        SavedAt = savedAt;
    }
}
