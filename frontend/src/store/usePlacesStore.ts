import { create } from 'zustand';
import type { PlaceDto, PlaceFilters, PlaceType } from '../types';
import { fetchPlaces } from '../api/places';

interface PlacesState {
  places: PlaceDto[];
  loading: boolean;
  error: string | null;
  filters: PlaceFilters;
  selectedPlaceId: string | null;
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
      const res = await fetchPlaces(filters);
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
