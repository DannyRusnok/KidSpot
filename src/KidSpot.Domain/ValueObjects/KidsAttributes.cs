namespace KidSpot.Domain.ValueObjects;

public record KidsAttributes(
    bool ChangingTable,
    bool KidsMenu,
    bool StrollerFriendly,
    int AgeFrom,
    int AgeTo
);
