export enum StatusEnum {
  Todo = 'ToDo',
  InProgress = 'InProgress',
  Done = 'Done',
}

export interface Task {
  id: number;
  title: string;
  authorId: number;
  performerId: number;
  projectId: number;
  priority: number;
  status: StatusEnum;
}

export interface TaskDto {
  title: string;
  authorId: number;
  performerId: number;
  projectId: number;
  priority: number;
  status: StatusEnum;
}
