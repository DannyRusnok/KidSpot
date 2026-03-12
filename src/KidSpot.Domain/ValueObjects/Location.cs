namespace KidSpot.Domain.ValueObjects;

public record Location(
    double Latitude,
    double Longitude,
    string Address,
    string City,
    string Country
);
