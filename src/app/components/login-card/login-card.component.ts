import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { loginUser } from 'src/app/models/loginUser';
import { AuthenticationService } from 'src/app/services/auth.service';
import { RouteValues } from '../../models/constants';

@Component({
  selector: 'app-login-card',
  templateUrl: './login-card.component.html',
  styleUrls: ['./login-card.component.scss'],
})
export class LoginCardComponent implements OnInit {
  loginForm!: FormGroup;
  isInputValid: boolean = false;
  loginStatus: boolean = false;
  loginMessage: string = '';
  error = '';

  responseUserObject?: loginUser;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: AuthenticationService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) {

  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      loginEmailControl: new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]+\\.[a-zA-Z]+@tss-yonder\\.com'),
      ]),
      loginPasswordControl: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ])
    });
  }

  isLoginFormCompleted(): boolean {
    return this.loginForm.touched;
  }

  isLoginFormValid(): boolean {
    return this.loginForm.valid;
  }

  isLoginFormDone(): boolean {
    return this.isLoginFormCompleted() && this.isLoginFormValid();
  }

  onLoginUser(): void {
    const user: loginUser = {
      email: this.loginForm.get('loginEmailControl')!.value,
      password: this.loginForm.get('loginPasswordControl')!.value,
    };
    this.loginService.login(user).subscribe({ 
      error: (error: Error) => {
        this.snackBar.open(error.message, '', {
          duration: 3000
        });
        
      },
        next: (data: loginUser) => {
        this.responseUserObject = { ...data };
        this.snackBar.open(`Login of user ${this.responseUserObject.name} successful!`,
          '',
          {
            duration: 3000
          })
        this.router.navigate([RouteValues.DASHBOARD]);
      }
    });
  }
}
