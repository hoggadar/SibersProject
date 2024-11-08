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