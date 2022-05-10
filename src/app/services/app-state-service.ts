import { Injectable } from "@angular/core";
import jwt_decode from 'jwt-decode';
import { BehaviorSubject } from "rxjs";
import { loginUser } from "../models/loginUser";

@Injectable({ providedIn: 'root' })
export class AppStateService {
    private readonly sessionInfo$: BehaviorSubject<loginUser | null> = new BehaviorSubject<loginUser | null>(<loginUser><unknown>localStorage.getItem('currentUser'));

    observerSessionInfo(): BehaviorSubject<loginUser | null> {
        return this.sessionInfo$;
    }

    getDecodedAccessToken(token: string): any {
        try {
            return jwt_decode(token);
        } catch (Error) {
            return null;
        }
    }

    updateSessionInfo(session: loginUser) {
        this.sessionInfo$.next(session);
    }

    getCurrentUserId(): number {
        const token = JSON.parse(localStorage.getItem('currentUser')).token;
        return this.getDecodedAccessToken(token).UserID; // decode token and get userId
    }
}