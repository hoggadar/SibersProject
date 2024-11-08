import axios from 'axios';

export const BASE_API = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
});
