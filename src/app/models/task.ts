import { SafeResourceUrl } from "@angular/platform-browser";

export interface Task {
	id: number;
	title: string;
	icon: string;
	defaultBackground: string;
	hasEvents: boolean;
}

export interface TaskResponse {
    result: Task;
}

export interface TasksResponse {
    result: Task[];
}

export interface ITask {
  [x: string]: any;
  id?: number;
  title: string;
  icon: string;
  defaultBackground: string;
  hasEvents:boolean;
}