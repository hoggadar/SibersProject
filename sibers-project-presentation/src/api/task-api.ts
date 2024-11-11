import { Task, TaskDto } from '../types/task-type.ts';
import BASE_API from './api.ts';

export const taskApi = {
  async getAllTasks(): Promise<Task[]> {
    return await BASE_API.get<Task[]>('/Task/get-all-tasks').then(
      (response) => response.data
    );
  },
  async getTasksByEmployeeId(employeeId: number): Promise<Task[]> {
    return await BASE_API.get<Task[]>(
      `/Task/get-tasks-by-employee/${employeeId}`
    ).then((response) => response.data);
  },
  async createTask(newTask: TaskDto): Promise<Task> {
    return await BASE_API.post<Task>('/Task/create-task', newTask).then(
      (response) => response.data
    );
  },
  async updateTask(id: number, updatedTask: TaskDto): Promise<Task> {
    return await BASE_API.put<Task>(
      `/Task/update-task/${id}`,
      updatedTask
    ).then((response) => response.data);
  },
  async deleteTask(id: number): Promise<Task> {
    return await BASE_API.delete<Task>(`/Task/delete-task/${id}`).then(
      (response) => response.data
    );
  },
};
