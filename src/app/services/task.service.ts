import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TasksResponse, ITask } from '../models/task';
import { catchError } from 'rxjs/operators';
import { HttpService } from './http.service';
import { RouteEndpoints } from '../models/constants';
import { ITaskPresence } from '../models/taskpresence';
@Injectable({
	providedIn: 'root',
})
export class TaskService {
	constructor(private httpService: HttpService) {}

	addNewTask(Task: ITask): Observable<ITask> {
		return this.httpService
			.post<ITask>(Task, RouteEndpoints.Task)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}

	deleteTask(id: number): Observable<ITask> {
		return this.httpService
			.delete<ITask>(id, `${RouteEndpoints.Task}?TaskId=`)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}

	getTask(id: number): Observable<ITask> {
		return this.httpService
			.getById<ITask>(id, RouteEndpoints.Task)
			.pipe(
				map((response) => response.result),
				catchError(this.httpService.handleHttpErrorResponse)
			);
	}

	updateTask(Task: ITask): Observable<ITask> {
		return this.httpService
			.update<ITask>(
				Task,
				`${RouteEndpoints.Task}?taskId=${Task.id}`
			)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}

	getTasks(): Observable<TasksResponse> {
		return this.httpService
			.getAll<TasksResponse>(RouteEndpoints.Task)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}
	getTasksForUser(userId:number): Observable<ITaskPresence> {
		return this.httpService
			.getTasksForUser<TasksResponse>(userId,RouteEndpoints.PRESENCE+"/GetUserTasks")
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}
}
