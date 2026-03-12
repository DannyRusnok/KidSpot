import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { fetchPlace } from '../api/places';
import { Reviews } from '../components/Reviews';
import { SavePlaceButton } from '../components/SavePlaceButton';
import type { PlaceDto } from '../types';

export function PlaceDetailPage() {
  const { id } = useParams<{ id: string }>();
  const [place, setPlace] = useState<PlaceDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    fetchPlace(id).then((res) => {
      if (res.data) {
        setPlace(res.data);
      } else {
        setError(res.error?.message ?? 'Place not found');
      }
      setLoading(false);
    });
  }, [id]);

  if (loading) return <div className="p-8 text-gray-500">Načítám...</div>;
  if (error || !place) return <div className="p-8 text-red-500">{error ?? 'Místo nebylo nalezeno.'}</div>;

  return (
    <div className="max-w-3xl mx-auto p-6">
      <Link to="/" className="text-sm text-indigo-600 hover:underline mb-4 inline-block">
        ← Zpět na mapu
      </Link>

      <div className="flex items-start justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">{place.name}</h1>
          <p className="text-sm text-gray-500 mt-1">
            {place.type} · {place.address}, {place.city}
          </p>
        </div>
        <SavePlaceButton placeId={place.id} />
      </div>

      {place.averageRating > 0 && (
        <p className="text-amber-600 font-medium mt-2">★ {place.averageRating.toFixed(1)}</p>
      )}

      <p className="text-gray-700 mt-4 leading-relaxed">{place.description}</p>

      <div className="mt-4 flex flex-wrap gap-3">
        <span className="px-3 py-1 bg-blue-50 text-blue-700 text-xs rounded-full font-medium">
          {place.ageFrom}–{place.ageTo} let
        </span>
        {place.changingTable && (
          <span className="px-3 py-1 bg-green-50 text-green-700 text-xs rounded-full font-medium">
            🚼 Přebalovárna
          </span>
        )}
        {place.kidsMenu && (
          <span className="px-3 py-1 bg-amber-50 text-amber-700 text-xs rounded-full font-medium">
            🍽️ Kids menu
          </span>
        )}
        {place.strollerFriendly && (
          <span className="px-3 py-1 bg-purple-50 text-purple-700 text-xs rounded-full font-medium">
            👶 Kočárek OK
          </span>
        )}
      </div>

      <Reviews placeId={place.id} />
    </div>
  );
}
