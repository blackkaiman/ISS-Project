import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { User } from '../models/user';
import { catchError } from 'rxjs/operators';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpService: HttpService) { 

  }

  register(user: User): Observable<User>{
    return this.httpService.post<User>(user, 'User').pipe(
      catchError(this.httpService.handleHttpErrorResponse)
    );
  }

}