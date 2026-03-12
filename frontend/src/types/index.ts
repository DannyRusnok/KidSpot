export interface ApiResponse<T> {
  data: T | null;
  error: ApiError | null;
}

export interface ApiError {
  code: string;
  message: string;
}

export type PlaceType =
  | 'Playground'
  | 'Restaurant'
  | 'Museum'
  | 'Beach'
  | 'Park'
  | 'Zoo'
  | 'Pool'
  | 'Other';

export interface PlaceDto {
  id: string;
  name: string;
  description: string;
  googlePlaceId: string | null;
  latitude: number;
  longitude: number;
  address: string;
  city: string;
  country: string;
  type: PlaceType;
  changingTable: boolean;
  kidsMenu: boolean;
  strollerFriendly: boolean;
  ageFrom: number;
  ageTo: number;
  averageRating: number;
  createdAt: string;
}

export interface ReviewDto {
  id: string;
  placeId: string;
  userId: string;
  userName: string;
  userAvatarUrl: string | null;
  rating: number;
  text: string;
  createdAt: string;
}

export interface UserDto {
  id: string;
  email: string;
  name: string;
  avatarUrl: string | null;
  isAdmin: boolean;
}

export interface AuthResult {
  token: string;
  user: UserDto;
}

export interface PlaceFilters {
  city: string;
  ageFrom?: number;
  ageTo?: number;
  type?: PlaceType;
}

export interface CreatePlaceRequest {
  name: string;
  description: string;
  latitude: number;
  longitude: number;
  address: string;
  city: string;
  country: string;
  type: PlaceType;
  changingTable: boolean;
  kidsMenu: boolean;
  strollerFriendly: boolean;
  ageFrom: number;
  ageTo: number;
  googlePlaceId?: string | null;
}
