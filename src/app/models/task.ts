import { SafeResourceUrl } from "@angular/platform-browser";

export interface Task {
	id: number;
	title: string;
}

export interface TaskResponse {
    result: Task;
}

export interface TasksResponse {
	[x: string]: any;
    result: Task[];
}

export interface ITask {
  [x: string]: any;
  id?: number;
  title: string;
}