import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

export const useAuthStore = defineStore('auth', () => {
  const accessToken = ref<string | null>(localStorage.getItem('accessToken'));

  const setToken = (token: string) => {
    accessToken.value = token;
    localStorage.setItem('accessToken', token);
  };

  const logout = () => {
    accessToken.value = null;
    localStorage.removeItem('accessToken');
  };

  const isAuthenticated = computed(() => !!accessToken.value);

  return { accessToken, setToken, logout, isAuthenticated };
});
