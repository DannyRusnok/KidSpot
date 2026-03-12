import api from './client';
import type { ApiResponse, AuthResult, UserDto } from '../types';

export async function loginWithGoogle(idToken: string) {
  const { data } = await api.post<ApiResponse<AuthResult>>('/auth/google', { idToken });
  return data;
}

export async function logout() {
  const { data } = await api.post<ApiResponse<{ message: string }>>('/auth/logout');
  return data;
}

export async function fetchMe() {
  const { data } = await api.get<ApiResponse<UserDto>>('/users/me');
  return data;
}

export async function savePlace(placeId: string) {
  const { data } = await api.post<ApiResponse<{ message: string }>>(
    `/users/me/saved-places/${placeId}`,
  );
  return data;
}

export async function removeSavedPlace(placeId: string) {
  const { data } = await api.delete<ApiResponse<{ message: string }>>(
    `/users/me/saved-places/${placeId}`,
  );
  return data;
}
