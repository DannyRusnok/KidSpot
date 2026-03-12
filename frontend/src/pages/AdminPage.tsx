import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from '../store/useAuthStore';
import { AddPlaceForm } from '../components/AddPlaceForm';
import { fetchPlaces } from '../api/places';
import type { PlaceDto } from '../types';

const PLACE_TYPE_LABELS: Record<string, string> = {
  Playground: 'Hriště',
  Restaurant: 'Restaurace',
  Museum: 'Muzeum',
  Beach: 'Pláž',
  Park: 'Park',
  Zoo: 'Zoo',
  Pool: 'Bazén',
  Other: 'Ostatní',
};

export function AdminPage() {
  const { user, loading } = useAuthStore();
  const navigate = useNavigate();
  const [places, setPlaces] = useState<PlaceDto[]>([]);
  const [cityFilter, setCityFilter] = useState('Praha');
  const [loadingPlaces, setLoadingPlaces] = useState(false);

  const loadPlaces = async () => {
    if (!cityFilter.trim()) return;
    setLoadingPlaces(true);
    try {
      const res = await fetchPlaces({ city: cityFilter });
      if (res.data) setPlaces(res.data);
    } catch {
      // ignore
    } finally {
      setLoadingPlaces(false);
    }
  };

  useEffect(() => {
    if (!loading && (!user || !user.isAdmin)) {
      navigate('/');
    }
  }, [user, loading, navigate]);

  useEffect(() => {
    loadPlaces();
  }, []);

  if (loading) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-8">
        <p className="text-gray-500">Načítám...</p>
      </div>
    );
  }

  if (!user?.isAdmin) return null;

  return (
    <div className="max-w-4xl mx-auto px-4 py-8 space-y-8">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-gray-900">Admin Panel</h1>
        <button
          onClick={() => navigate('/')}
          className="text-sm text-indigo-600 hover:text-indigo-800"
        >
          Zpět na mapu
        </button>
      </div>

      <AddPlaceForm onCreated={loadPlaces} />

      {/* Existing places list */}
      <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
        <h2 className="text-lg font-semibold text-gray-900 mb-4">Existující místa</h2>

        <div className="flex gap-2 mb-4">
          <input
            type="text"
            value={cityFilter}
            onChange={(e) => setCityFilter(e.target.value)}
            placeholder="Město"
            className="flex-1 px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            onKeyDown={(e) => {
              if (e.key === 'Enter') loadPlaces();
            }}
          />
          <button
            onClick={loadPlaces}
            disabled={loadingPlaces}
            className="px-4 py-2 bg-gray-100 text-gray-700 text-sm font-medium rounded-md hover:bg-gray-200 disabled:opacity-50"
          >
            {loadingPlaces ? 'Načítám...' : 'Načíst'}
          </button>
        </div>

        {places.length === 0 ? (
          <p className="text-sm text-gray-500">Žádná místa pro toto město.</p>
        ) : (
          <div className="space-y-2">
            {places.map((place) => (
              <div
                key={place.id}
                className="flex items-center justify-between p-3 bg-gray-50 rounded-md border border-gray-100"
              >
                <div>
                  <p className="text-sm font-medium text-gray-900">{place.name}</p>
                  <p className="text-xs text-gray-500">
                    {PLACE_TYPE_LABELS[place.type] || place.type} · {place.city} · {place.ageFrom}–{place.ageTo} let
                    {place.changingTable && ' · Přebalovárna'}
                    {place.kidsMenu && ' · Dětské menu'}
                    {place.strollerFriendly && ' · Kočárek'}
                  </p>
                </div>
                <button
                  onClick={() => navigate(`/place/${place.id}`)}
                  className="text-xs text-indigo-600 hover:underline"
                >
                  Detail
                </button>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
