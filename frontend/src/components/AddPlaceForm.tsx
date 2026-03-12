import { useState } from 'react';
import type { CreatePlaceRequest, PlaceType } from '../types';
import { createPlace } from '../api/places';

const PLACE_TYPES: { value: PlaceType; label: string }[] = [
  { value: 'Playground', label: 'Hriště' },
  { value: 'Restaurant', label: 'Restaurace' },
  { value: 'Museum', label: 'Muzeum' },
  { value: 'Beach', label: 'Pláž' },
  { value: 'Park', label: 'Park' },
  { value: 'Zoo', label: 'Zoo' },
  { value: 'Pool', label: 'Bazén' },
  { value: 'Other', label: 'Ostatní' },
];

const EMPTY_FORM: CreatePlaceRequest = {
  name: '',
  description: '',
  latitude: 0,
  longitude: 0,
  address: '',
  city: '',
  country: 'Česko',
  type: 'Playground',
  changingTable: false,
  kidsMenu: false,
  strollerFriendly: false,
  ageFrom: 0,
  ageTo: 18,
  googlePlaceId: null,
};

interface Props {
  onCreated: () => void;
}

export function AddPlaceForm({ onCreated }: Props) {
  const [form, setForm] = useState<CreatePlaceRequest>({ ...EMPTY_FORM });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  const [addressQuery, setAddressQuery] = useState('');
  const [geocoding, setGeocoding] = useState(false);

  const set = <K extends keyof CreatePlaceRequest>(key: K, value: CreatePlaceRequest[K]) =>
    setForm((prev) => ({ ...prev, [key]: value }));

  const geocodeAddress = async () => {
    if (!addressQuery.trim()) return;
    setGeocoding(true);
    try {
      const res = await fetch(
        `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(addressQuery)}&format=json&limit=1&addressdetails=1`,
        { headers: { 'Accept-Language': 'cs,en' } },
      );
      const data = await res.json();
      if (data.length > 0) {
        const item = data[0];
        const addr = item.address || {};
        setForm((prev) => ({
          ...prev,
          latitude: parseFloat(item.lat),
          longitude: parseFloat(item.lon),
          address: item.display_name?.split(',').slice(0, 3).join(',').trim() || addressQuery,
          city: addr.city || addr.town || addr.village || addr.municipality || prev.city,
          country: addr.country || prev.country,
        }));
        setError(null);
      } else {
        setError('Adresa nenalezena. Zkuste jiný dotaz.');
      }
    } catch {
      setError('Chyba při vyhledávání adresy.');
    } finally {
      setGeocoding(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.name.trim() || !form.city.trim()) {
      setError('Název a město jsou povinné.');
      return;
    }
    if (form.latitude === 0 && form.longitude === 0) {
      setError('Vyhledejte adresu pro nastavení souřadnic.');
      return;
    }

    setSaving(true);
    setError(null);
    setSuccess(false);

    try {
      const res = await createPlace(form);
      if (res.error) {
        setError(res.error.message);
      } else {
        setSuccess(true);
        setForm({ ...EMPTY_FORM });
        setAddressQuery('');
        onCreated();
      }
    } catch {
      setError('Nepodařilo se uložit místo.');
    } finally {
      setSaving(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="bg-white rounded-lg shadow-sm border border-gray-200 p-6 space-y-5">
      <h2 className="text-lg font-semibold text-gray-900">Přidat nové místo</h2>

      {error && (
        <div className="bg-red-50 border border-red-200 text-red-700 text-sm rounded-md px-4 py-2">{error}</div>
      )}
      {success && (
        <div className="bg-green-50 border border-green-200 text-green-700 text-sm rounded-md px-4 py-2">
          Místo bylo úspěšně přidáno!
        </div>
      )}

      {/* Address geocoding */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Vyhledat adresu</label>
        <div className="flex gap-2">
          <input
            type="text"
            value={addressQuery}
            onChange={(e) => setAddressQuery(e.target.value)}
            placeholder="např. Petřínská rozhledna, Praha"
            className="flex-1 px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            onKeyDown={(e) => {
              if (e.key === 'Enter') {
                e.preventDefault();
                geocodeAddress();
              }
            }}
          />
          <button
            type="button"
            onClick={geocodeAddress}
            disabled={geocoding}
            className="px-4 py-2 bg-gray-100 text-gray-700 text-sm font-medium rounded-md hover:bg-gray-200 disabled:opacity-50"
          >
            {geocoding ? 'Hledám...' : 'Najít'}
          </button>
        </div>
        {form.latitude !== 0 && (
          <p className="text-xs text-gray-500 mt-1">
            {form.address} ({form.latitude.toFixed(5)}, {form.longitude.toFixed(5)})
          </p>
        )}
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {/* Name */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Název *</label>
          <input
            type="text"
            value={form.name}
            onChange={(e) => set('name', e.target.value)}
            placeholder="Název místa"
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            required
          />
        </div>

        {/* Type */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Typ místa</label>
          <select
            value={form.type}
            onChange={(e) => set('type', e.target.value as PlaceType)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            {PLACE_TYPES.map((t) => (
              <option key={t.value} value={t.value}>{t.label}</option>
            ))}
          </select>
        </div>

        {/* City */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Město *</label>
          <input
            type="text"
            value={form.city}
            onChange={(e) => set('city', e.target.value)}
            placeholder="Praha"
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            required
          />
        </div>

        {/* Country */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Země</label>
          <input
            type="text"
            value={form.country}
            onChange={(e) => set('country', e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        {/* Age range */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Věk od</label>
          <input
            type="number"
            min={0}
            max={18}
            value={form.ageFrom}
            onChange={(e) => set('ageFrom', Number(e.target.value))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Věk do</label>
          <input
            type="number"
            min={0}
            max={18}
            value={form.ageTo}
            onChange={(e) => set('ageTo', Number(e.target.value))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
      </div>

      {/* Description */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Popis</label>
        <textarea
          value={form.description}
          onChange={(e) => set('description', e.target.value)}
          rows={3}
          placeholder="Krátký popis místa..."
          className="w-full px-3 py-2 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
        />
      </div>

      {/* Kids attributes */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">Vybavení</label>
        <div className="flex flex-wrap gap-4">
          <label className="flex items-center gap-2 text-sm text-gray-700">
            <input
              type="checkbox"
              checked={form.changingTable}
              onChange={(e) => set('changingTable', e.target.checked)}
              className="rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
            />
            Přebalovárna
          </label>
          <label className="flex items-center gap-2 text-sm text-gray-700">
            <input
              type="checkbox"
              checked={form.kidsMenu}
              onChange={(e) => set('kidsMenu', e.target.checked)}
              className="rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
            />
            Dětské menu
          </label>
          <label className="flex items-center gap-2 text-sm text-gray-700">
            <input
              type="checkbox"
              checked={form.strollerFriendly}
              onChange={(e) => set('strollerFriendly', e.target.checked)}
              className="rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
            />
            Kočárek-friendly
          </label>
        </div>
      </div>

      <button
        type="submit"
        disabled={saving}
        className="w-full px-4 py-2 bg-indigo-600 text-white text-sm font-medium rounded-md hover:bg-indigo-700 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        {saving ? 'Ukládám...' : 'Přidat místo'}
      </button>
    </form>
  );
}
