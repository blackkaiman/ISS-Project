import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { TasksResponse, Task } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class EndpointsService {
  constructor(private http: HttpClient) { }


  getTasks(): Observable<TasksResponse> {
    return this.http.get<TasksResponse>(environment.apiUrl + "Task") 
  }
  
  deleteTask(TaskId: number): Observable<unknown> {
    return this.http.delete(environment.apiUrl + "Task", { params: { TaskId: TaskId.toString() } });
  }
}
