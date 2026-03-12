namespace KidSpot.Domain.Entities;

public class User
{
    private readonly List<SavedPlace> _savedPlaces = new();

    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string? AvatarUrl { get; private set; }
    public string GoogleId { get; private set; } = null!;
    public bool IsAdmin { get; private set; }
    public IReadOnlyList<SavedPlace> SavedPlaces => _savedPlaces.AsReadOnly();
    public DateTime CreatedAt { get; private set; }

    private User() { } // EF Core constructor

    public static User Create(string email, string name, string googleId, string? avatarUrl = null)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = name,
            GoogleId = googleId,
            AvatarUrl = avatarUrl,
            IsAdmin = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProfile(string name, string? avatarUrl)
    {
        Name = name;
        AvatarUrl = avatarUrl;
    }

    public void SavePlace(Guid placeId)
    {
        if (_savedPlaces.All(sp => sp.PlaceId != placeId))
        {
            _savedPlaces.Add(new SavedPlace(Id, placeId, DateTime.UtcNow));
        }
    }

    public void RemoveSavedPlace(Guid placeId)
    {
        var saved = _savedPlaces.FirstOrDefault(sp => sp.PlaceId == placeId);
        if (saved is not null)
        {
            _savedPlaces.Remove(saved);
        }
    }
}
