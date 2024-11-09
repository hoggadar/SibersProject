import { LoginDto, SignupDto, TokenDto } from '../types/auth-type.ts';
import BASE_API from './api.ts';
import { useAuthStore } from '../store/authStore.ts';

export const authApi = {
  async login(dto: LoginDto): Promise<void> {
    const authStore = useAuthStore();
    try {
      const response = await BASE_API.post<TokenDto>('/Auth/login', dto);
      const token = response.data.token;
      authStore.setToken(token);
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    }
  },
  async signup(dto: SignupDto): Promise<void> {
    const authStore = useAuthStore();
    try {
      const response = await BASE_API.post<TokenDto>('/Auth/signup', dto);
      const token = response.data.token;
      authStore.setToken(token);
    } catch (error) {
      console.error('Signup error:', error);
      throw error;
    }
  },
};
