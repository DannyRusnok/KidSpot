import api from './client';
import type { ApiResponse, PlaceDto, PlaceFilters } from '../types';

export async function fetchPlaces(filters: PlaceFilters) {
  const params = new URLSearchParams({ city: filters.city });
  if (filters.ageFrom !== undefined) params.set('ageFrom', String(filters.ageFrom));
  if (filters.ageTo !== undefined) params.set('ageTo', String(filters.ageTo));
  if (filters.type) params.set('type', filters.type);

  const { data } = await api.get<ApiResponse<PlaceDto[]>>(`/places?${params}`);
  return data;
}

export async function fetchPlace(id: string) {
  const { data } = await api.get<ApiResponse<PlaceDto>>(`/places/${id}`);
  return data;
}
