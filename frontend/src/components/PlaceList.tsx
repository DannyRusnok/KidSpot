import { Link } from 'react-router-dom';
import { usePlacesStore } from '../store/usePlacesStore';

export function PlaceList() {
  const { places, loading, error } = usePlacesStore();

  if (loading) return <p className="text-sm text-gray-500 p-4">Načítám místa...</p>;
  if (error) return <p className="text-sm text-red-500 p-4">{error}</p>;
  if (places.length === 0) return <p className="text-sm text-gray-400 p-4">Zadej město a vyhledej kids-friendly místa.</p>;

  return (
    <div className="flex flex-col gap-2 overflow-y-auto">
      {places.map((place) => (
        <Link
          key={place.id}
          to={`/place/${place.id}`}
          className="block p-3 rounded-lg border border-gray-200 hover:border-indigo-300 hover:shadow-sm transition-all"
        >
          <div className="flex items-start justify-between">
            <div>
              <h3 className="text-sm font-semibold text-gray-800">{place.name}</h3>
              <p className="text-xs text-gray-500 mt-0.5">{place.type} · {place.ageFrom}–{place.ageTo} let</p>
            </div>
            {place.averageRating > 0 && (
              <span className="text-xs font-medium text-amber-600">★ {place.averageRating.toFixed(1)}</span>
            )}
          </div>
          <div className="flex gap-3 mt-1.5 text-xs text-gray-400">
            {place.changingTable && <span>🚼 Přebalovárna</span>}
            {place.kidsMenu && <span>🍽️ Kids menu</span>}
            {place.strollerFriendly && <span>👶 Kočárek OK</span>}
          </div>
        </Link>
      ))}
    </div>
  );
}
