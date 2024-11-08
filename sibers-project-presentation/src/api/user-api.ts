import { User, UserDto } from '../types/user-type.ts';
import { BASE_API } from './api.ts';

export const userApi = {
  async getAllUsers(): Promise<User[]> {
    return await BASE_API.get<User[]>('/User/get-all-users').then(
      (response) => response.data
    );
  },
  async getUsersByProjectId(projectId: number): Promise<User[]> {
    return await BASE_API.get<User[]>(
      `/User/get-users-by-project/${projectId}`
    ).then((response) => response.data);
  },
  async createUser(newUser: UserDto): Promise<User> {
    return await BASE_API.post<User>('/User/create-user', newUser).then(
      (response) => response.data
    );
  },
  async updateUser(id: number, updatedUser: UserDto): Promise<User> {
    return await BASE_API.put<User>(
      `/User/update-user/${id}`,
      updatedUser
    ).then((response) => response.data);
  },
  async deleteUser(id: number): Promise<User> {
    return await BASE_API.delete<User>(`/User/delete-user/${id}`).then(
      (response) => response.data
    );
  },
};
