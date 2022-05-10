import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { ITaskPresence } from '../models/taskpresence';
import { RouteEndpoints } from '../models/constants';
import { User, UserDetails } from '../models/user';
import { HttpService } from './http.service';
import { ITask } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskPresenceService {

  constructor(private httpService: HttpService) {}

	addNewPresence(presence: ITaskPresence): Observable<ITaskPresence> {
		return this.httpService
			.post<ITaskPresence>(presence, RouteEndpoints.PRESENCE)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}
  
  	getPresenceForTask(taskId: number): Observable<UserDetails[]> {
		return this.httpService
			.getById<UserDetails[]>(taskId, RouteEndpoints.PRESENCE_GET_PARTICIPANTS)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}

	deletePresence(taskId: number, userId: number): Observable<ITaskPresence> {
		return this.httpService
			.deleteByTwoId<ITaskPresence>(taskId, userId, RouteEndpoints.PRESENCE)
			.pipe(catchError(this.httpService.handleHttpErrorResponse));
	}

	
}
