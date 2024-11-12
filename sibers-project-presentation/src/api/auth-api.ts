import { LoginDto, SignupDto, TokenDto } from '../types/auth-type.ts';
import BASE_API from './api.ts';
import { useAuthStore } from '../store/authStore.ts';

export const authApi = {
  async login(dto: LoginDto): Promise<void> {
    const authStore = useAuthStore();
    try {
      const response = await BASE_API.post<TokenDto>('/Auth/login', dto);
      const token = response.data.accessToken;
      const role = response.data.role;
      const id = response.data.id;
      authStore.setToken(token);
      authStore.setRole(role);
      authStore.setId(id);
      console.log(response);
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    }
  },
  async signup(dto: SignupDto): Promise<void> {
    const authStore = useAuthStore();
    try {
      const response = await BASE_API.post<TokenDto>('/Auth/signup', dto);
      const token = response.data.accessToken;
      const role = response.data.role;
      const id = response.data.id;
      authStore.setToken(token);
      authStore.setRole(role);
      authStore.setId(id);
    } catch (error) {
      console.error('Signup error:', error);
      throw error;
    }
  },
};
