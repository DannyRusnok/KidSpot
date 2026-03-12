# KidSpot — CLAUDE.md

## Co je KidSpot

Webová aplikace pro rodiče cestující s dětmi. Vrstva nad Google Maps která přidává
kids-specific data: filtrování podle věku dítěte, praktické atributy (přebalovárna,
kids menu, kočárek-friendly), recenze od rodičů.

Uživatel zadá město → vidí mapu s kurátorovanými kids-friendly místy → filtruje →
ukládá do trip planneru.

---

## Tech Stack

### Frontend
- React 18 + TypeScript
- Tailwind CSS (žádný jiný CSS framework)
- Vite
- Google Maps JavaScript API (@vis.gl/react-google-maps)
- React Router v6
- Axios pro API calls
- Zustand pro state management

### Backend
- .NET 8 / ASP.NET Core Web API
- C#
- PostgreSQL (přes Entity Framework Core)
- Google Places API (v Infrastructure vrstvě)
- Google OAuth 2.0 (přihlášení přes Google)

---

## Architektura backendu

Striktně domain-centric, DDD přístup. Žádné zkratky.

```
src/
  KidSpot.Domain/
    Entities/         ← Place, User, Review
    ValueObjects/     ← Location, KidsAttributes, PlaceType
    Events/           ← Domain events
    Repositories/     ← Interfaces (IPlaceRepository, IUserRepository...)

  KidSpot.Application/
    Places/
      Queries/        ← GetPlacesByCity, GetPlaceDetail, SearchPlaces
      Commands/       ← CreatePlace, UpdatePlace (admin only)
    Users/
      Commands/       ← AuthenticateWithGoogle, SavePlace, RemovePlace
    Reviews/
      Commands/       ← AddReview
      Queries/        ← GetReviewsByPlace

  KidSpot.Infrastructure/
    Persistence/      ← EF Core DbContext, Migrations, Repositories impl.
    GooglePlaces/     ← GooglePlacesService (IExternalPlacesService)
    Auth/             ← Google OAuth handler

  KidSpot.API/
    Controllers/      ← PlacesController, UsersController, ReviewsController
    Middleware/       ← Auth middleware, Error handling
    Program.cs
```

---

## Domain Model

### Place (hlavní entita)
```csharp
public class Place
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? GooglePlaceId { get; private set; }
    public Location Location { get; private set; }       // Value Object
    public PlaceType Type { get; private set; }          // Enum
    public KidsAttributes KidsAttributes { get; private set; } // Value Object
    public Guid CuratedBy { get; private set; }
    public IReadOnlyList<Review> Reviews { get; private set; }
    public double AverageRating { get; private set; }
}
```

### KidsAttributes (Value Object)
```csharp
public record KidsAttributes(
    bool ChangingTable,
    bool KidsMenu,
    bool StrollerFriendly,
    int AgeFrom,
    int AgeTo
);
```

### PlaceType (Enum)
```csharp
public enum PlaceType
{
    Playground, Restaurant, Museum, Beach, Park, Zoo, Pool, Other
}
```

### User
```csharp
public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string GoogleId { get; private set; }
    public bool IsAdmin { get; private set; }
    public IReadOnlyList<Guid> SavedPlaceIds { get; private set; }
}
```

---

## API Endpoints

```
GET    /api/places?city={city}&ageFrom={n}&ageTo={n}&type={type}
GET    /api/places/{id}
POST   /api/places              ← admin only
PUT    /api/places/{id}         ← admin only

GET    /api/places/{id}/reviews
POST   /api/places/{id}/reviews ← auth required

GET    /api/users/me
POST   /api/users/me/saved-places/{placeId}
DELETE /api/users/me/saved-places/{placeId}

POST   /api/auth/google         ← Google OAuth callback
POST   /api/auth/logout
```

---

## Coding Rules

- **Vždy private setters** na Domain entitách, factory methods nebo konstruktory
- **Žádná business logika v Controllerech** — jen volání Application layer
- **Interfaces v Domain** pro repository, implementace v Infrastructure
- **Value Objects jsou immutable** — record types
- **Tailwind only** — žádný inline style, žádný custom CSS pokud to nejde Tailwindem
- **Anglicky** — veškerý kód, komentáře, názvy proměnných
- **Česky** — commit messages, PR descriptions (pro Daniela)
- Každý endpoint vrací konzistentní response wrapper:
```json
{ "data": ..., "error": null }
{ "data": null, "error": { "code": "NOT_FOUND", "message": "..." } }
```

---

## Databáze (PostgreSQL)

```sql
-- Hlavní tabulky
places (id, name, description, google_place_id, lat, lng, address, city, country, type,
        changing_table, kids_menu, stroller_friendly, age_from, age_to,
        curated_by, average_rating, created_at, updated_at)

users (id, email, name, avatar_url, google_id, is_admin, created_at)

saved_places (user_id, place_id, saved_at)

reviews (id, place_id, user_id, rating, text, created_at)
```

---

## Auth flow (Google OAuth)

1. FE přesměruje na Google consent screen
2. Google callback → POST /api/auth/google s `code`
3. Backend vymění code za access token + id token
4. Vytvoří nebo aktualizuje User záznam
5. Vrátí JWT token (7 dní platnost)
6. FE uloží JWT do httpOnly cookie

---

## Prostředí

- Backend: http://localhost:5000
- Frontend: http://localhost:5173
- PostgreSQL: localhost:5432, db: kidspot

---

## Co je MVP (v tomto pořadí stavíme)

1. Backend scaffolding — solution structure, EF Core, DB migrace
2. Google OAuth — přihlášení
3. Places API — GET endpoints (čtení míst)
4. Frontend — mapa + piny + filtrování
5. Detail místa
6. Trip planner (save/unsave)
7. Admin panel — přidávání míst
8. Reviews

---

## Co není součástí MVP

- Offline mode (service worker) — Fáze 2
- AI import z Google Places — Fáze 2
- Komunita / UGC — Fáze 3
- Platby / monetizace — Fáze 3