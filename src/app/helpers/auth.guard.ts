import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Role } from '../models/constants';
import { AuthenticationService } from '../services/auth.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationService.currentUserValue;
        if (currentUser) {
            // logged in so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}

@Injectable({ providedIn: 'root' })
export class AdminGuard implements CanActivate {
    constructor(
        private router: Router
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        var userRole = JSON.parse(localStorage.getItem("currentUser")).role;
        if (userRole == Role.Admin) {
            // admin
            return true;
        }

        // normal user
        this.router.navigate(['/dashboard'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}

@Injectable({ providedIn: 'root' })
export class UserGuard implements CanActivate {
    constructor(
        private router: Router
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        var userRole = JSON.parse(localStorage.getItem("currentUser")).role;
        if (userRole == Role.User) {
            // normal user
            return true;
        }
        // admin
        this.router.navigate(['/administrate-tasks'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}