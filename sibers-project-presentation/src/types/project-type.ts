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

export interface AddEmployeeToProjectDto {
    EmployeeId: number;
    ProjectId: number;
}

export interface RemoveEmployeeFromProjectDto {
    EmployeeId: number;
    ProjectId: number;
}