import { useState } from 'react';
import { usePlacesStore } from '../store/usePlacesStore';
import type { PlaceType } from '../types';

const PLACE_TYPES: PlaceType[] = [
  'Playground',
  'Restaurant',
  'Museum',
  'Beach',
  'Park',
  'Zoo',
  'Pool',
  'Other',
];

const PLACE_TYPE_LABELS: Record<PlaceType, string> = {
  Playground: 'Hřiště',
  Restaurant: 'Restaurace',
  Museum: 'Muzeum',
  Beach: 'Pláž',
  Park: 'Park',
  Zoo: 'Zoo',
  Pool: 'Bazén',
  Other: 'Ostatní',
};

export function SearchBar() {
  const { filters, setCity, setAgeRange, setType, search, loading } = usePlacesStore();
  const [ageFrom, setAgeFrom] = useState('');
  const [ageTo, setAgeTo] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setAgeRange(
      ageFrom ? Number(ageFrom) : undefined,
      ageTo ? Number(ageTo) : undefined,
    );
    search();
  };

  return (
    <form onSubmit={handleSubmit} className="bg-white p-4 rounded-lg shadow-sm border border-gray-200">
      <div className="flex flex-wrap gap-3 items-end">
        <div className="flex-1 min-w-[200px]">
          <label className="block text-xs font-medium text-gray-500 mb-1">Město</label>
          <input
            type="text"
            value={filters.city}
            onChange={(e) => setCity(e.target.value)}
            placeholder="např. Praha"
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="w-24">
          <label className="block text-xs font-medium text-gray-500 mb-1">Věk od</label>
          <input
            type="number"
            min={0}
            max={18}
            value={ageFrom}
            onChange={(e) => setAgeFrom(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="w-24">
          <label className="block text-xs font-medium text-gray-500 mb-1">Věk do</label>
          <input
            type="number"
            min={0}
            max={18}
            value={ageTo}
            onChange={(e) => setAgeTo(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="w-40">
          <label className="block text-xs font-medium text-gray-500 mb-1">Typ místa</label>
          <select
            value={filters.type ?? ''}
            onChange={(e) => setType((e.target.value || undefined) as PlaceType | undefined)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Všechny</option>
            {PLACE_TYPES.map((t) => (
              <option key={t} value={t}>
                {PLACE_TYPE_LABELS[t]}
              </option>
            ))}
          </select>
        </div>
        <button
          type="submit"
          disabled={loading || !filters.city.trim()}
          className="px-5 py-2 bg-indigo-600 text-white text-sm font-medium rounded-md hover:bg-indigo-700 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {loading ? 'Hledám...' : 'Hledat'}
        </button>
      </div>
    </form>
  );
}
