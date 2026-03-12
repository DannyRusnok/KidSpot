import { create } from 'zustand';
import type { UserDto } from '../types';
import * as authApi from '../api/auth';

interface AuthState {
  user: UserDto | null;
  loading: boolean;
  setUser: (user: UserDto | null) => void;
  login: (idToken: string) => Promise<void>;
  logout: () => Promise<void>;
  fetchMe: () => Promise<void>;
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  loading: true,

  setUser: (user) => set({ user }),

  login: async (idToken: string) => {
    const res = await authApi.loginWithGoogle(idToken);
    if (res.data) {
      set({ user: res.data.user });
    }
  },

  logout: async () => {
    await authApi.logout();
    set({ user: null });
  },

  fetchMe: async () => {
    try {
      const res = await authApi.fetchMe();
      set({ user: res.data, loading: false });
    } catch {
      set({ user: null, loading: false });
    }
  },
}));
