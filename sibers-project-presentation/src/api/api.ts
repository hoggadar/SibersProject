import axios from 'axios';
import { useAuthStore } from '../store/authStore.ts';

const BASE_API = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

BASE_API.interceptors.request.use((config) => {
  const authStore = useAuthStore();
  if (authStore.isAuthenticated && config.headers) {
    config.headers['Authorization'] = `Bearer ${authStore.accessToken}`;
  }
  return config;
});

export default BASE_API;
