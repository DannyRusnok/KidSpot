import { create } from 'zustand';
import type { PlaceDto, PlaceFilters, PlaceType } from '../types';
import { fetchPlaces } from '../api/places';

interface CityCenter {
  lat: number;
  lng: number;
}

async function geocodeCity(city: string): Promise<CityCenter | null> {
  try {
    const res = await fetch(
      `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(city)}&format=json&limit=1`,
      { headers: { 'Accept-Language': 'cs,en' } },
    );
    const data = await res.json();
    if (data.length > 0) {
      return { lat: parseFloat(data[0].lat), lng: parseFloat(data[0].lon) };
    }
    return null;
  } catch {
    return null;
  }
}

interface PlacesState {
  places: PlaceDto[];
  loading: boolean;
  error: string | null;
  filters: PlaceFilters;
  selectedPlaceId: string | null;
  cityCenter: CityCenter | null;
  setCity: (city: string) => void;
  setAgeRange: (ageFrom?: number, ageTo?: number) => void;
  setType: (type?: PlaceType) => void;
  setSelectedPlaceId: (id: string | null) => void;
  search: () => Promise<void>;
}

export const usePlacesStore = create<PlacesState>((set, get) => ({
  places: [],
  loading: false,
  error: null,
  filters: { city: '' },
  selectedPlaceId: null,
  cityCenter: null,

  setCity: (city) => set((s) => ({ filters: { ...s.filters, city } })),
  setAgeRange: (ageFrom, ageTo) =>
    set((s) => ({ filters: { ...s.filters, ageFrom, ageTo } })),
  setType: (type) => set((s) => ({ filters: { ...s.filters, type } })),
  setSelectedPlaceId: (id) => set({ selectedPlaceId: id }),

  search: async () => {
    const { filters } = get();
    if (!filters.city.trim()) return;

    set({ loading: true, error: null });
    try {
      const [res, center] = await Promise.all([
        fetchPlaces(filters),
        geocodeCity(filters.city),
      ]);

      if (center) {
        set({ cityCenter: center });
      }

      if (res.data) {
        set({ places: res.data, loading: false });
      } else {
        set({ error: res.error?.message ?? 'Unknown error', loading: false });
      }
    } catch {
      set({ error: 'Failed to fetch places', loading: false });
    }
  },
}));
