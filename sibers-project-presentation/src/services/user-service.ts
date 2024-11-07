import axios from 'axios';

// const API_URL = 'http://localhost:5004/api';

export enum RoleEnum {
    Director = 'Director',
    ProjectManager = 'ProjectManager',
    Employee = 'Employee',
}

export interface User {
    id: number;
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;
    password: string;
    role: RoleEnum;
}

export interface UserDto {
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;
    password: string;
    role: RoleEnum;
}

export const userService = {
    async getAllUsers(): Promise<User[]> {
        try
        {
            const response = await axios.get<User[]>("http://localhost:5004/api/User/get-all-users");
            return response.data;
        } catch (error) {
            console.error('Ошибка при получении пользователей:', error);
            throw error;
        }
    },
    async createUser(newUser: UserDto): Promise<User> {
        try
        {
            const response = await axios.post<User>("http://localhost:5004/api/User/create-user", newUser);
            return response.data;
        } catch (error) {
            console.error('Ошибка при создании пользователя:', error);
            throw error;
        }
    },
    async updateUser(id: number, updatedUser: UserDto): Promise<User> {
        try {
            const response = await axios.put<User>(`http://localhost:5004/api/User/update-user/${id}`, updatedUser);
            return response.data;
        } catch (error) {
            console.error('Ошибка при обновлении пользователя:', error);
            throw error;
        }
    },
    async deleteUser(id: number): Promise<void> {
        try {
            await axios.delete(`http://localhost:5004/api/User/delete-user/${id}`);
            console.log('Пользователь успешно удален');
        } catch (error) {
            console.error('Ошибка при удалении пользователя:', error);
            throw error;
        }
    }
};
