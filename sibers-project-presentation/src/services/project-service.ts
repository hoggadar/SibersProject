import axios from "axios";

export interface Project {
    id: number;
    title: string;
    customerCompany: string;
    performerCompany: string;
    directorId: number;
    startDate: Date;
    endDate: Date;
    priority: number;
}

export interface ProjectDto {
    id: number;
    title: string;
    customerCompany: string;
    performerCompany: string;
    directorId: number;
    priority: number;
}

export interface CreateProjectDto {
    title: string;
    customerCompany: string;
    performerCompany: string;
    directorId: number;
    priority: number;
}

export interface UpdateProjectDto {
    title: string;
    customerCompany: string;
    performerCompany: string;
    directorId: number;
    priority: number;
}

export const projectService = {
    async getAllProjects(): Promise<Project[]> {
        try {
            const response = await axios.get<Project[]>("http://localhost:5004/api/Project/get-all-projects");
            return response.data;
        } catch (error) {
            console.error('Ошибка при получении проектов:', error);
            throw error;
        }
    },
    async createProject(newProject: CreateProjectDto): Promise<Project> {
        try {
            const response = await axios.post<Project>("http://localhost:5004/api/Project/create-project", newProject);
            return response.data;
        } catch (error) {
            console.error('Ошибка при создании проекта:', error);
            throw error;
        }
    },
    async updateProject(id: number, updatedProject: UpdateProjectDto): Promise<Project> {
        try {
            const response = await axios.put<Project>(`http://localhost:5004/api/Project/update-project/${id}`, updatedProject);
            return response.data;
        } catch (error) {
            console.error('Ошибка при обновлении проекта:', error);
            throw error;
        }
    },
    async deleteProject(id: number): Promise<void> {
        try {
            await axios.delete(`http://localhost:5004/api/Project/delete-project/${id}`);
            console.log('Проект успешно удален');
        } catch (error) {
            console.error('Ошибка при удалении проекта:', error);
            throw error;
        }
    }
}
