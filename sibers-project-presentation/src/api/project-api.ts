import {
  AddEmployeeToProjectDto,
  CreateProjectDto,
  Project,
  RemoveEmployeeFromProjectDto,
  UpdateProjectDto,
} from '../types/project-type.ts';
import BASE_API from './api.ts';

export const projectApi = {
  async getAllProjects(): Promise<Project[]> {
    return await BASE_API.get<Project[]>('/Project/get-all-projects').then(
      (response) => response.data
    );
  },
  async createProject(dto: CreateProjectDto): Promise<Project> {
    return await BASE_API.post<Project>('/Project/create-project', dto).then(
      (response) => response.data
    );
  },
  async updateProject(id: number, dto: UpdateProjectDto): Promise<Project> {
    return await BASE_API.put<Project>(
      `/Project/update-project/${id}`,
      dto
    ).then((response) => response.data);
  },
  async deleteProject(id: number): Promise<Project> {
    return await BASE_API.delete<Project>(`/Project/delete-project/${id}`).then(
      (response) => response.data
    );
  },
  async uploadFiles(projectId: number, files: File[]): Promise<any> {
    const formData = new FormData();
    files.forEach((file) => {
      formData.append('files', file);
    });
    return await BASE_API.post(
      `/Project/upload-documents/${projectId}`,
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      }
    ).then((response) => response.data);
  },
  async addEmployeeToProject(dto: AddEmployeeToProjectDto): Promise<void> {
    return await BASE_API.post('/Project/add-employee-to-project', dto).then(
      (response) => response.data
    );
  },
  async removeEmployeeFromProject(
    dto: RemoveEmployeeFromProjectDto
  ): Promise<void> {
    return await BASE_API.post(
      '/Project/remove-employee-from-project',
      dto
    ).then((response) => response.data);
  },
};
