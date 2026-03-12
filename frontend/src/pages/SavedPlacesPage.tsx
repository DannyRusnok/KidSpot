import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from '../store/useAuthStore';
import { fetchSavedPlaces, removeSavedPlace } from '../api/auth';
import type { PlaceDto } from '../types';

const PLACE_TYPE_LABELS: Record<string, string> = {
  Playground: 'Hřiště',
  Restaurant: 'Restaurace',
  Museum: 'Muzeum',
  Beach: 'Pláž',
  Park: 'Park',
  Zoo: 'Zoo',
  Pool: 'Bazén',
  Other: 'Ostatní',
};

export function SavedPlacesPage() {
  const { user, loading: authLoading } = useAuthStore();
  const navigate = useNavigate();
  const [places, setPlaces] = useState<PlaceDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [removing, setRemoving] = useState<string | null>(null);

  useEffect(() => {
    if (!authLoading && !user) {
      navigate('/');
    }
  }, [user, authLoading, navigate]);

  useEffect(() => {
    if (user) {
      loadSaved();
    }
  }, [user]);

  const loadSaved = async () => {
    setLoading(true);
    try {
      const res = await fetchSavedPlaces();
      if (res.data) setPlaces(res.data);
    } catch {
      // ignore
    } finally {
      setLoading(false);
    }
  };

  const handleRemove = async (placeId: string) => {
    setRemoving(placeId);
    try {
      await removeSavedPlace(placeId);
      setPlaces((prev) => prev.filter((p) => p.id !== placeId));
    } catch {
      // ignore
    } finally {
      setRemoving(null);
    }
  };

  if (authLoading || loading) {
    return (
      <div className="max-w-3xl mx-auto px-4 py-8">
        <p className="text-gray-500">Načítám...</p>
      </div>
    );
  }

  if (!user) return null;

  // Group places by city
  const byCity = places.reduce<Record<string, PlaceDto[]>>((acc, place) => {
    const city = place.city || 'Ostatní';
    if (!acc[city]) acc[city] = [];
    acc[city].push(place);
    return acc;
  }, {});

  const cities = Object.keys(byCity).sort();

  return (
    <div className="max-w-3xl mx-auto px-4 py-8 space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Moje místa</h1>
          <p className="text-sm text-gray-500 mt-1">
            {places.length === 0
              ? 'Zatím nemáte žádná uložená místa.'
              : `${places.length} uložených míst`}
          </p>
        </div>
        <button
          onClick={() => navigate('/')}
          className="text-sm text-indigo-600 hover:text-indigo-800"
        >
          Zpět na mapu
        </button>
      </div>

      {places.length === 0 && (
        <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-8 text-center">
          <p className="text-gray-500 mb-4">
            Najděte zajímavá místa na mapě a uložte si je sem pro svůj výlet.
          </p>
          <button
            onClick={() => navigate('/')}
            className="px-4 py-2 bg-indigo-600 text-white text-sm font-medium rounded-md hover:bg-indigo-700"
          >
            Prozkoumat mapu
          </button>
        </div>
      )}

      {cities.map((city) => (
        <div key={city}>
          <h2 className="text-lg font-semibold text-gray-800 mb-2">{city}</h2>
          <div className="space-y-2">
            {byCity[city].map((place) => (
              <div
                key={place.id}
                className="bg-white rounded-lg shadow-sm border border-gray-200 p-4 flex items-center justify-between"
              >
                <div
                  className="flex-1 cursor-pointer"
                  onClick={() => navigate(`/place/${place.id}`)}
                >
                  <div className="flex items-center gap-2">
                    <h3 className="text-sm font-medium text-gray-900">{place.name}</h3>
                    {place.averageRating > 0 && (
                      <span className="text-xs text-amber-600">
                        ★ {place.averageRating.toFixed(1)}
                      </span>
                    )}
                  </div>
                  <p className="text-xs text-gray-500 mt-1">
                    {PLACE_TYPE_LABELS[place.type] || place.type} · {place.ageFrom}–{place.ageTo} let
                    {place.changingTable && ' · 🚼 Přebalovárna'}
                    {place.kidsMenu && ' · 🍽️ Dětské menu'}
                    {place.strollerFriendly && ' · 👶 Kočárek'}
                  </p>
                  {place.description && (
                    <p className="text-xs text-gray-400 mt-1 line-clamp-1">{place.description}</p>
                  )}
                </div>
                <button
                  onClick={() => handleRemove(place.id)}
                  disabled={removing === place.id}
                  className="ml-3 text-xs text-red-500 hover:text-red-700 disabled:opacity-50 shrink-0"
                >
                  {removing === place.id ? 'Odstraňuji...' : 'Odebrat'}
                </button>
              </div>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
}
