import api from './client';
import type { ApiResponse, ReviewDto } from '../types';

export async function fetchReviews(placeId: string) {
  const { data } = await api.get<ApiResponse<ReviewDto[]>>(`/places/${placeId}/reviews`);
  return data;
}

export async function addReview(placeId: string, rating: number, text: string) {
  const { data } = await api.post<ApiResponse<ReviewDto>>(`/places/${placeId}/reviews`, {
    rating,
    text,
  });
  return data;
}
