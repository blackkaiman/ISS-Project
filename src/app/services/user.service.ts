import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, UserDetails, UserUpdate, UserUpdatePresence } from '../models/user';
import { environment } from 'src/environments/environment';

import { catchError, Observable } from 'rxjs';
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient, private httpService: HttpService) { }

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/GetPresent`);
    }

    getAllUsers(): Observable<UserDetails[]> {
        return this.httpService
            .getAll<UserDetails>('User/GetPresent')
            .pipe(catchError(this.httpService.handleHttpErrorResponse));
    }
    
    getUserById(userId: number): Observable<UserDetails> {
        return this.httpService
            .getById<UserDetails>(userId, 'User/Id')
            .pipe(catchError(this.httpService.handleHttpErrorResponse))
    }

    putUser(userUpdate: UserUpdate): Observable<UserUpdate> {
        return this.httpService
            .update<UserUpdate>(userUpdate, 'User')
            .pipe(catchError(this.httpService.handleHttpErrorResponse))
    }
    putUserPresence(userUpdate: UserUpdatePresence): Observable<UserUpdate> {
        return this.httpService
            .update<UserUpdatePresence>(userUpdate, 'User/updatePresence')
            .pipe(catchError(this.httpService.handleHttpErrorResponse))
    }
}
