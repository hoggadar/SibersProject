import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

export const useAuthStore = defineStore('auth', () => {
  const id = ref<string | null>(localStorage.getItem('userId'));
  const accessToken = ref<string | null>(localStorage.getItem('accessToken'));
  const role = ref<string | null>(localStorage.getItem('userRole'));

  const setToken = (token: string) => {
    accessToken.value = token;
    localStorage.setItem('accessToken', token);
  };

  const setRole = (userRole: string) => {
    role.value = userRole;
    localStorage.setItem('userRole', userRole);
  };

  const setId = (userId: string) => {
    id.value = userId;
    localStorage.setItem('userId', userId);
  };

  const logout = () => {
    accessToken.value = null;
    role.value = null;
    localStorage.removeItem('userId');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('userRole');
  };

  const isAuthenticated = computed(() => !!accessToken.value);

  return {
    accessToken,
    setToken,
    setRole,
    setId,
    logout,
    isAuthenticated,
    role,
    id,
  };
});
