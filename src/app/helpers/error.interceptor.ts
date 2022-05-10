import { Injectable } from '@angular/core';
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/auth.service';
import { RouteValues } from '../models/constants';
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
	constructor(
		private authenticationService: AuthenticationService,
		private router: Router
	) {}

	intercept(
		request: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		return next.handle(request).pipe(
			catchError((err) => {
				switch (err.status) {
					case 401:
						this.authenticationService.logout();
						this.router.navigate([RouteValues.LOGIN])
						break;
					case 404:
						this.router.navigate([RouteValues.NOT_FOUND]);
						break;
				}

				const error = {
					message: err.error || err.statusText,
				};
				return throwError(() => error);
			})
		);
	}
}
