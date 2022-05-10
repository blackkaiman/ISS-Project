import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginCardComponent } from './components/login-card/login-card.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ConfirmComponent } from './components/dialogs/confirm/confirm.component';
import { RegisterCardComponent } from './components/register-card/register-card.component';
import { MaterialModules } from './modules/material.module';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { TasksTableComponent } from './components/tasks-table/tasks-table.component';
import { TaskCardComponent } from './components/task-card/task-card.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NotFoundComponent } from './components/not-found/not-found.component';



@NgModule({
	declarations: [
		AppComponent,
		LoginCardComponent,
		RegisterCardComponent,
		ConfirmComponent,
		RegisterCardComponent,
		LoginCardComponent,
		TasksTableComponent,
		TaskCardComponent,
		DashboardComponent,
        NotFoundComponent,
    ],
    imports: [
        BrowserModule,
        FormsModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        MaterialModules,
        ReactiveFormsModule,
        HttpClientModule,
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        MatDatepickerModule,
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
